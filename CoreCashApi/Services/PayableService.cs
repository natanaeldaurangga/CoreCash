using CoreCashApi.Data;
using CoreCashApi.DTOs.Pagination;
using CoreCashApi.DTOs.Records;
using CoreCashApi.Entities;
using CoreCashApi.Enums;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace CoreCashApi.Services
{
    public class PayableBalance
    {
        public Guid UserId { get; set; }

        public Record? Record { get; set; }

        public int AccountCode { get; set; }

        public Guid CreditorId { get; set; }

        public string CreditorName { get; set; } = string.Empty;

        public string CreditorEmail { get; set; } = string.Empty;

        public decimal Balance { get; set; }
    }

    public class PayableService
    {
        private readonly AppDbContext _dbContext;

        private readonly IConfiguration _config;

        private readonly ILogger<PayableService> _logger;

        public PayableService(AppDbContext dbContext, IConfiguration config, ILogger<PayableService> logger)
        {
            _dbContext = dbContext;
            _config = config;
            _logger = logger;
        }

        public async Task<ResponsePayableDetail?> GetPayableDetailAsync(Guid userId, Guid CreditorId, RequestPagination request, TrashFilter trashFilter = TrashFilter.WITHOUT_TRASHED)
        {
            var Creditor = await _dbContext.Contacts!.FirstOrDefaultAsync(ct => ct.Id.Equals(CreditorId));

            if (Creditor == default) return null;

            var query = _dbContext.Records!
            .Include(rc => rc.Ledgers)!
                .ThenInclude(lg => lg.Account)
            .Include(rc => rc.PayableLedger)
            .AsQueryable();

            query = query
            .Where(rc => rc.UserId.Equals(userId))
            .Where(rc => rc.PayableLedger!.CreditorId.Equals(CreditorId))
            .Where(rc =>
                trashFilter == TrashFilter.WITHOUT_TRASHED ? rc.DeletedAt == default :
                trashFilter != TrashFilter.ONLY_TRASHED || rc.DeletedAt != default
            );

            query = query.OrderBy(rc => rc.RecordedAt);

            query = query.Take(1000);

            int totalData = query.Count();

            query = query.Skip((request.CurrentPage - 1) * request.PageSize).Take(request.PageSize);

            var result = await query.Select(rc => new ResponseRecord()
            {
                RecordId = rc.Id,
                TransactionDate = rc.RecordedAt,
                Entry = rc.Ledgers!.FirstOrDefault(lg => lg.Account!.AccountCode == AccountCodes.PAYABLE)!.Entry,
                Balance = rc.Ledgers!.FirstOrDefault()!.Balance
            })
            .AsNoTracking()
            .ToListAsync();

            float totalPageDec = (float)totalData / request.PageSize;
            int totalPage = (int)Math.Ceiling(totalPageDec);

            var records = new ResponsePagination<ResponseRecord>()
            {
                PageSize = request.PageSize,
                CurrentPage = request.CurrentPage,
                TotalPages = totalPage,
                Items = result
            };

            return new ResponsePayableDetail()
            {
                CreditorId = CreditorId,
                Creditor = new ResponseContact
                {
                    Id = Creditor!.Id,
                    Name = Creditor!.Name,
                    Email = Creditor!.Email,
                    PhoneNumber = Creditor!.PhoneNumber
                },
                Records = records
            };
        }

        public async Task<ResponsePagination<ResponsePayable>?> GetRecordPagedAsync(Guid userId, RequestPagination request, TrashFilter trashFilter = TrashFilter.WITHOUT_TRASHED)
        {
            var query = _dbContext.Records!
            .Include(rc => rc.Ledgers)!
                .ThenInclude(lg => lg.Account)
            .Include(rc => rc.PayableLedger)!
                .ThenInclude(rl => rl!.Creditor)
            .Where(rc =>
                rc.UserId.Equals(userId) &&
                (
                    EF.Functions.Like(rc.PayableLedger!.Creditor!.Name, $"%{request.Keyword}%") ||
                    EF.Functions.Like(rc.PayableLedger!.Creditor!.Email, $"%{request.Keyword}%")
                )
            )
            .Where(rc =>
                trashFilter == TrashFilter.WITHOUT_TRASHED ? rc.DeletedAt == default :
                trashFilter != TrashFilter.ONLY_TRASHED || rc.DeletedAt != default
            )
            .Where(rc =>
                rc.RecordGroup == RecordGroup.NEW_PAYABLE ||
                rc.RecordGroup == RecordGroup.PAYABLE_PAYMENT
            )
            .SelectMany(rc => rc.Ledgers!,
                (rc, lg) => new PayableBalance
                {
                    UserId = rc.UserId,
                    Record = rc,
                    AccountCode = lg.Account!.AccountCode,
                    CreditorId = rc.PayableLedger!.CreditorId,
                    CreditorName = rc.PayableLedger!.Creditor!.Name,
                    CreditorEmail = rc.PayableLedger!.Creditor!.Email,
                    Balance = lg.Entry == Entry.CREDIT ? lg.Balance : -lg.Balance
                }
            )
            .Where(rb => rb.AccountCode == AccountCodes.PAYABLE)
            .GroupBy(rb => rb.CreditorId)
            .Select(group => new ResponsePayable()
            {
                RecordId = group.Select(rb => rb.Record!.Id).FirstOrDefault(),
                TransactionDate = group.Select(rb => rb.Record!.RecordedAt).FirstOrDefault(),
                CreditorId = group.Select(rb => rb.CreditorId).FirstOrDefault(),
                CreditorName = group.Select(rb => rb.CreditorName).FirstOrDefault() ?? "",
                CreditorEmail = group.Select(rb => rb.CreditorEmail).FirstOrDefault() ?? "",
                Balance = group.Sum(rb => rb.Balance)
            });

            var sortBy = string.IsNullOrEmpty(request.SortBy) ? nameof(Record.RecordedAt) : request.SortBy;
            var direction = request.Direction.Equals("ASC", StringComparison.OrdinalIgnoreCase) ? "ASC" : "DESC";

            if (!string.IsNullOrEmpty(sortBy) && !string.IsNullOrWhiteSpace(sortBy) && direction != null)
            {
                var sortExpression = $"{sortBy} {direction}";
                query = query.OrderBy(sortExpression);
            }

            query = query.Take(1000);

            int totalData = query.Count();

            query = query.Skip((request.CurrentPage - 1) * request.PageSize).Take(request.PageSize);

            var result = await query.AsNoTracking().ToListAsync();

            float totalPageDec = (float)totalData / request.PageSize;
            int totalPage = (int)Math.Ceiling(totalPageDec);

            return new ResponsePagination<ResponsePayable>()
            {
                PageSize = request.PageSize,
                CurrentPage = request.CurrentPage,
                TotalPages = totalPage,
                Items = result
            };
        }

        public async Task<int> PaymentRecordAsync(Guid userId, RequestPayablePayment request)
        {
            using var transaction = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                var Creditor = await _dbContext.Contacts!.FirstOrDefaultAsync(ct => ct.Id.Equals(request.CreditorId));
                if (Creditor == null) return 0;

                // START: Creating New Record
                var record = new Record()
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    RecordGroup = RecordGroup.PAYABLE_PAYMENT,
                    RecordedAt = request.TransactionDate,
                    Description = request.Description,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                await _dbContext.Records!.AddAsync(record);
                await _dbContext.SaveChangesAsync();
                // END: Creating New Record

                // START: Creatint New Payable
                var Payable = new PayableLedger()
                {
                    RecordId = record.Id,
                    CreditorId = request.CreditorId,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                await _dbContext.PayableLedgers!.AddAsync(Payable);
                await _dbContext.SaveChangesAsync();
                // END: Creating New Payable

                // START: Creating New Ledger
                var accPayable = await _dbContext.Accounts!.FirstOrDefaultAsync(acc => acc.AccountCode == AccountCodes.PAYABLE);

                var ledger = new Ledger()
                {
                    Id = Guid.NewGuid(),
                    RecordId = record.Id,
                    AccountId = accPayable!.Id,
                    Entry = Entry.DEBIT,
                    Balance = request.Balance,
                };

                await _dbContext.Ledgers!.AddAsync(ledger);
                await _dbContext.SaveChangesAsync();
                // END: Creating New Ledger

                // START: Insert to cash if payment via cash
                if (request.FromCash)
                {
                    var accCash = await _dbContext.Accounts!.FirstOrDefaultAsync(acc => acc.AccountCode == AccountCodes.CASH);

                    var cashLedger = new Ledger()
                    {
                        Id = Guid.NewGuid(),
                        RecordId = record.Id,
                        AccountId = accCash!.Id,
                        Entry = Entry.CREDIT,
                        Balance = request.Balance
                    };

                    await _dbContext.Ledgers!.AddAsync(cashLedger);
                    await _dbContext.SaveChangesAsync();
                }
                // END: Insert to cash if payment via cash

                await transaction.CommitAsync();
                _logger.LogInformation("commit");
                return 1;
            }
            catch (System.Exception)
            {
                await transaction.RollbackAsync();
                _logger.LogInformation("Berhasil di rollback");
                throw;
            }
        }

        public async Task<int> NewPayableAsync(Guid userId, RequestPayableRecord request)
        {
            using var transaction = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                var Creditor = await _dbContext.Contacts!.FirstOrDefaultAsync(ct => ct.Id.Equals(request.CreditorId));
                if (Creditor == null) return 0;
                // START: Creating New Record
                var record = new Record()
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    RecordGroup = RecordGroup.NEW_PAYABLE,
                    RecordedAt = request.TransactionDate,
                    Description = request.Description,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                await _dbContext.Records!.AddAsync(record);
                await _dbContext.SaveChangesAsync();
                // END: Creating New Record

                // START: Creatint New Payable
                var Payable = new PayableLedger()
                {
                    RecordId = record.Id,
                    CreditorId = request.CreditorId,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                await _dbContext.PayableLedgers!.AddAsync(Payable);
                await _dbContext.SaveChangesAsync();
                // END: Creating New Payable

                // START: Creating New Ledger
                var account = await _dbContext.Accounts!.FirstOrDefaultAsync(acc => acc.AccountCode == AccountCodes.PAYABLE);

                var ledger = new Ledger()
                {
                    Id = Guid.NewGuid(),
                    RecordId = record.Id,
                    AccountId = account!.Id,
                    Entry = Entry.CREDIT,
                    Balance = request.Balance,
                };

                await _dbContext.Ledgers!.AddAsync(ledger);
                await _dbContext.SaveChangesAsync();
                // END: Creating New Ledger

                await transaction.CommitAsync();
                return 1;
            }
            catch (System.Exception)
            {
                await transaction.RollbackAsync();
                _logger.LogInformation("Berhasil di rollback");
                throw;
            }
        }
    }
}
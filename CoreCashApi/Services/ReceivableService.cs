using CoreCashApi.Data;
using CoreCashApi.DTOs.Pagination;
using CoreCashApi.DTOs.Records;
using CoreCashApi.Entities;
using CoreCashApi.Enums;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace CoreCashApi.Services
{
    public class ReceivableBalance
    {
        public Guid UserId { get; set; }

        public Record? Record { get; set; }

        public Guid DebtorId { get; set; }

        public string DebtorName { get; set; } = string.Empty;

        public string DebtorEmail { get; set; } = string.Empty;

        public decimal Balance { get; set; }
    }

    public class ReceivableService
    {
        private readonly AppDbContext _dbContext;

        private readonly IConfiguration _config;

        private readonly ILogger<ReceivableService> _logger;

        public ReceivableService(AppDbContext dbContext, IConfiguration config, ILogger<ReceivableService> logger)
        {
            _dbContext = dbContext;
            _config = config;
            _logger = logger;
        }

        public async Task<ResponseReceivableDetail?> GetReceivableDetailAsync(Guid userId, Guid debtorId, RequestPagination request, TrashFilter trashFilter = TrashFilter.WITHOUT_TRASHED)
        {
            var query = _dbContext.Records!
            .Include(rc => rc.Ledgers)
            .Include(rc => rc.ReceivableLedger)
            .AsQueryable();

            query = query
            .Where(rc => rc.UserId.Equals(userId))
            .Where(rc => rc.ReceivableLedger!.DebtorId.Equals(debtorId))
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
                Entry = rc.Ledgers!.FirstOrDefault()!.Entry,
                Balance = rc.Ledgers!.FirstOrDefault()!.Balance
            }).ToListAsync();

            float totalPageDec = (float)totalData / request.PageSize;
            int totalPage = (int)Math.Ceiling(totalPageDec);

            var records = new ResponsePagination<ResponseRecord>()
            {
                PageSize = request.PageSize,
                CurrentPage = request.CurrentPage,
                TotalPages = totalPage,
                Items = result
            };

            var debtor = await _dbContext.Contacts!.FirstOrDefaultAsync(ct => ct.Id.Equals(debtorId));

            return new ResponseReceivableDetail()
            {
                DebtorId = debtorId,
                Debtor = new ResponseContact
                {
                    Id = debtor!.Id,
                    Name = debtor!.Name,
                    Email = debtor!.Email,
                    PhoneNumber = debtor!.PhoneNumber
                },
                Records = records
            };
        }

        public async Task<ResponsePagination<ResponseReceivable>?> GetRecordPagedAsync(Guid userId, RequestPagination request, TrashFilter trashFilter = TrashFilter.WITHOUT_TRASHED)
        {
            // TODO: Pelajari complex query ef core untuk bikin pagination buat receivable paged
            var query = _dbContext.Records!
            .Include(rc => rc.Ledgers)
            .Include(rc => rc.ReceivableLedger)!
                .ThenInclude(rl => rl!.Debtor)
            .Where(rc =>
                rc.UserId.Equals(userId) &&
                (
                    EF.Functions.Like(rc.ReceivableLedger!.Debtor!.Name, $"%{request.Keyword}%") ||
                    EF.Functions.Like(rc.ReceivableLedger!.Debtor!.Email, $"%{request.Keyword}%")
                )
            )
            .Where(rc =>
                trashFilter == TrashFilter.WITHOUT_TRASHED ? rc.DeletedAt == default :
                trashFilter != TrashFilter.ONLY_TRASHED || rc.DeletedAt != default)
            .SelectMany(rc => rc.Ledgers!,
                (rc, lg) => new ReceivableBalance
                {
                    UserId = rc.UserId,
                    Record = rc,
                    DebtorId = rc.ReceivableLedger!.DebtorId,
                    DebtorName = rc.ReceivableLedger!.Debtor!.Name,
                    DebtorEmail = rc.ReceivableLedger!.Debtor!.Email,
                    Balance = lg.Entry == Entry.CREDIT ? lg.Balance : -lg.Balance
                }
            )
            .GroupBy(rb => rb.DebtorId)
            .Select(group => new ResponseReceivable()
            {
                RecordId = group.Select(rb => rb.Record!.Id).FirstOrDefault(),
                TransactionDate = group.Select(rb => rb.Record!.RecordedAt).FirstOrDefault(),
                DebtorId = group.Select(rb => rb.DebtorId).FirstOrDefault(),
                DebtorName = group.Select(rb => rb.DebtorName).FirstOrDefault() ?? "",
                DebtorEmail = group.Select(rb => rb.DebtorEmail).FirstOrDefault() ?? "",
                Balance = group.Sum(rb => rb.Balance)
            });

            var sortBy = request.SortBy;
            var direction = request.Direction.Equals("ASC", StringComparison.OrdinalIgnoreCase) ? "ASC" : "DESC";

            if (!string.IsNullOrEmpty(sortBy) && !string.IsNullOrWhiteSpace(sortBy) && direction != null)
            {
                var sortExpression = $"{sortBy} {direction}";
                query = query.OrderBy(sortExpression);
            }

            query = query.Take(1000);

            int totalData = query.Count();

            query = query.Skip((request.CurrentPage - 1) * request.PageSize).Take(request.PageSize);

            var result = await query.ToListAsync();

            float totalPageDec = (float)totalData / request.PageSize;
            int totalPage = (int)Math.Ceiling(totalPageDec);

            return new ResponsePagination<ResponseReceivable>()
            {
                PageSize = request.PageSize,
                CurrentPage = request.CurrentPage,
                TotalPages = totalPage,
                Items = result
            };
        }

        public async Task<int> InsertNewRecordAsync(Guid userId, RequestReceivableRecord request)
        {
            using var transaction = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                // START: Creating New Record
                _logger.LogInformation("start insert record");
                var record = new Record()
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    RecordGroup = RecordGroup.NEW_RECEIVABLE,
                    RecordedAt = request.TransactionDate,
                    Description = request.Description,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                await _dbContext.Records!.AddAsync(record);
                await _dbContext.SaveChangesAsync();
                _logger.LogInformation("finish insert record");
                // END: Creating New Record

                // START: Creatint New Receivable
                _logger.LogInformation("start insert receivable");
                var receivable = new ReceivableLedger()
                {
                    RecordId = record.Id,
                    DebtorId = request.DebtorId,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                await _dbContext.ReceivableLedgers!.AddAsync(receivable);
                await _dbContext.SaveChangesAsync();
                _logger.LogInformation("end insert receivable");
                // END: Creating New Receivable

                // START: Creating New Ledger
                _logger.LogInformation("start insert ledger");
                var account = await _dbContext.Accounts!.FirstOrDefaultAsync(acc => acc.AccountCode == AccountCodes.RECEIVABLE);

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
                _logger.LogInformation("finish insert ledger");
                // END: Creating New Ledger

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
    }
}
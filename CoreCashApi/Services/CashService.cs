using System;
using CoreCashApi.Data;
using CoreCashApi.DTOs.Pagination;
using CoreCashApi.DTOs.Records;
using CoreCashApi.Entities;
using CoreCashApi.Enums;
using CoreCashApi.Utilities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace CoreCashApi.Services
{
    public class CashSumCondition
    {
        public Guid UserId { get; set; }
        public int AccountCode { get; set; }
        public decimal Balance { get; set; }
    }

    public class CashService
    {
        private readonly AppDbContext _dbContext;

        private readonly IConfiguration _config;

        private readonly ILogger<CashService> _logger;

        public CashService(AppDbContext dbContext, IConfiguration config, ILogger<CashService> logger)
        {
            _dbContext = dbContext;
            _config = config;
            _logger = logger;
        }

        public async Task<ResponseCashBalanceSum> GetTotalBalanceAsync(Guid userId)
        {
            var balanceSum = await _dbContext.Records!
            .Include(rc => rc.Ledgers)!
                .ThenInclude(lg => lg.Account)
            .Where(rc => rc.UserId.Equals(userId) && rc.DeletedAt == default)
            .SelectMany(rc => rc.Ledgers!,
                (rc, lg) => new CashSumCondition
                {
                    UserId = rc.UserId,
                    AccountCode = lg.Account!.AccountCode,
                    Balance = lg.Entry == Entry.DEBIT ? lg.Balance : -lg.Balance
                }
            )
            .Where(csc => csc.AccountCode == AccountCodes.CASH)
            .GroupBy(summary => summary.UserId)
            .Select(group => new ResponseCashBalanceSum
            {
                UserId = group.Key,
                TotalBalance = group.Sum(summary => summary.Balance)
            })
            .ToListAsync();

            return balanceSum.FirstOrDefault()!;
        }

        public async Task<int> ForceDeleteRecordAsync(Guid userId, Guid recordId)
        {
            using var transaction = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                var ledger = await _dbContext.Ledgers!.FirstOrDefaultAsync(ld => ld.RecordId.Equals(recordId));

                _dbContext.Ledgers!.Remove(ledger!);
                await _dbContext.SaveChangesAsync();

                var record = await _dbContext.Records!.FirstOrDefaultAsync(rc => rc.Id.Equals(recordId) && rc.UserId.Equals(userId));

                if (record == null) return 0;

                _dbContext.Records!.Remove(record!);
                await _dbContext.SaveChangesAsync();

                await transaction.CommitAsync();
                return 1;
            }
            catch (System.Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<int> RestoreRecordAsync(Guid userId, Guid recordId)
        {
            return await SetDeletedAtAsync(userId, recordId, null);
        }

        public async Task<int> SoftDeleteRecordAsync(Guid userId, Guid recordId)
        {
            return await SetDeletedAtAsync(userId, recordId, DateTime.UtcNow);
        }

        public async Task<int> SetDeletedAtAsync(Guid userId, Guid recordId, DateTime? dateTime = null)
        {
            var record = await _dbContext.Records!.FirstOrDefaultAsync(rc => rc.UserId.Equals(userId) && rc.Id.Equals(recordId));

            if (record == default) return 0;

            record.DeletedAt = dateTime;

            await _dbContext.SaveChangesAsync();
            return 1;
        }

        public async Task<ResponseRecordDetail?> GetCashRecordsDetailAsync(Guid userId, Guid recordId)
        {
            var record = await _dbContext.Records!.Include(rc => rc.Ledgers)!.ThenInclude(ld => ld.Account).FirstOrDefaultAsync(rc => rc.Id.Equals(recordId) && rc.UserId.Equals(userId) && rc.DeletedAt == default);

            if (record == null) return null;

            var ledgerResponses = record!
            .Ledgers!.Select(ld => new ResponseLedger()
            {
                AccountId = ld.Account!.AccountCode,
                Entry = ld.Entry,
                Balance = ld.Balance
            }).ToList();

            return new ResponseRecordDetail()
            {
                RecordId = record!.Id,
                UserId = userId,
                Description = record!.Description,
                TransactionDate = record!.RecordedAt,
                Ledgers = ledgerResponses
            };
        }

        public async Task<ResponsePagination<ResponseRecord>?> GetCashRecordsPagedAsync(Guid userId, RequestRecordPagination request, TrashFilter trashFilter = TrashFilter.WITHOUT_TRASHED)
        {
            var query = _dbContext.Records!.Include(rc => rc.User).AsQueryable();

            query = query.Where(rc => rc.UserId.Equals(userId));

            query = trashFilter switch
            {
                TrashFilter.WITHOUT_TRASHED => query.Where(rc => rc.DeletedAt == default),
                TrashFilter.ONLY_TRASHED => query.Where(rc => rc.DeletedAt != default),
                _ => query
            };

            // Kondisi range tanggal transaksi
            if (request.StartDate != default && request.EndDate != default)
            {
                query = query.Where(rc =>
                    rc.RecordedAt.Millisecond >= request.StartDate.Millisecond
                    && rc.RecordedAt.Millisecond <= request.EndDate.Millisecond
                );
            }

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

            var result = await query.Select(rc => new ResponseRecord()
            {
                RecordId = rc.Id,
                TransactionDate = rc.RecordedAt,
                Entry = rc.Ledgers!.FirstOrDefault()!.Entry,
                Balance = rc.Ledgers!.FirstOrDefault()!.Balance
            }).ToListAsync();

            float totalPageDec = (float)totalData / request.PageSize;
            int totalPage = (int)Math.Ceiling(totalPageDec);

            return new ResponsePagination<ResponseRecord>()
            {
                PageSize = request.PageSize,
                CurrentPage = request.CurrentPage,
                TotalPages = totalPage,
                Direction = direction ?? "",
                SortBy = sortBy,
                Items = result
            };
        }

        public async Task<int> InsertNewRecordAsync(Guid userId, RequestCashRecord request)
        {
            using var transaction = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                var recordGroup = request.Entry == Entry.DEBIT ? RecordGroup.CASH_IN : RecordGroup.CASH_OUT;
                var record = new Record()
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    RecordGroup = recordGroup,
                    RecordedAt = request.TransactionDate,
                    Description = request.Description,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                };

                // Mencari nomor akun yang akan dijurnal
                var account = await _dbContext.Accounts!.FirstOrDefaultAsync(acc => acc.AccountCode.Equals(AccountCodes.CASH));

                var ledger = new Ledger()
                {
                    Id = Guid.NewGuid(),
                    Account = account,
                    AccountId = account!.Id,
                    Entry = request.Entry,
                    Balance = request.Balance,
                    RecordId = record.Id,
                    Record = record
                };

                _dbContext.Records!.Add(record);
                _dbContext.Ledgers!.Add(ledger);

                await _dbContext.SaveChangesAsync();

                await transaction.CommitAsync();
                return 1;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
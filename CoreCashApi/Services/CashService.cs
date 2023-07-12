using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class CashService
    {
        protected readonly AppDbContext _dbContext;

        protected readonly IConfiguration _config;

        protected readonly ImageUtility _imageUtil;

        public CashService(AppDbContext dbContext, IConfiguration config, ImageUtility imageUtil)
        {
            _dbContext = dbContext;
            _config = config;
            _imageUtil = imageUtil;
        }

        // TODO: Lanjut bikin pagination buat tiap Cash Record
        public async Task<ResponsePagination<ResponseRecord>?> GetCashRecordsPagedAsync(Guid userId, RequestPagination request, TrashFilter trashFilter = TrashFilter.WITHOUT_TRASHED)
        {
            var query = _dbContext.Records!.Include(rc => rc.User).Include(rc => rc.Ledgers).AsQueryable();

            query = query.Where(rc => rc.UserId.Equals(userId));

            query = trashFilter switch
            {
                TrashFilter.WITHOUT_TRASHED => query.Where(rc => rc.DeletedAt == default),
                TrashFilter.ONLY_TRASHED => query.Where(rc => rc.DeletedAt != default),
                _ => query
            };

            var sortBy = request.SortBy;
            var direction = request.Direction;

            if (!string.IsNullOrEmpty(sortBy) && !string.IsNullOrWhiteSpace(sortBy) && direction != null)
            {
                sortBy = "records.id";
                direction = SortDirection.ASC;
                var sortExpression = $"{sortBy} {direction}";
                query = query.OrderBy(sortExpression);
            }

            // FIXME: Sequence contains no element
            query = query.Take(1000);

            int totalData = query.Count();

            query = query.Skip((request.CurrentPage - 1) * request.PageSize).Take(request.PageSize);

            var cashRecord = query.Select(rc => rc.Ledgers!.First()).First();

            var result = await query.Select(rc => new ResponseRecord()
            {
                RecordId = rc.Id,
                TransactionDate = rc.RecordedAt,
                Entry = cashRecord.Entry,
                Balance = cashRecord.Balance
            }).ToListAsync();

            float totalPageDec = (float)totalData / request.PageSize;
            int totalPage = (int)Math.Ceiling(totalPageDec);

            return new ResponsePagination<ResponseRecord>()
            {
                PageSize = request.PageSize,
                CurrentPage = request.CurrentPage,
                TotalPages = totalPage,
                Items = result
            };
        }

        public async Task<int> InsertNewRecordAsync(Guid userId, RequestCashRecord request)
        {
            using var transaction = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                // var recordType = ;
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
                return 0;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
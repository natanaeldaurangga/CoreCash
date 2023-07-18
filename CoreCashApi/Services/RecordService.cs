using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreCashApi.Data;
using CoreCashApi.DTOs.Records;
using Microsoft.EntityFrameworkCore;

namespace CoreCashApi.Services
{
    public class RecordService
    {
        // TODO: Bikin servis khusus untuk soft delete, force delete, sama restore untuk tiap record,
        // jadi nggak usah diulang ulang di bagian receivable dan payable
        private readonly AppDbContext _dbContext;

        private readonly ILogger<RecordService> _logger;

        public RecordService(AppDbContext dbContext, ILogger<RecordService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<ResponseRecordDetail?> GetRecordDetailAsync(Guid userId, Guid recordId)
        {
            try
            {
                var record = await _dbContext.Records!
                .Include(rc => rc.Ledgers)!
                    .ThenInclude(lg => lg.Account)
                .FirstOrDefaultAsync(rc => rc.Id.Equals(recordId) && rc.UserId.Equals(userId));

                if (record == default) return null;

                return new ResponseRecordDetail()
                {
                    UserId = userId,
                    RecordId = recordId,
                    TransactionDate = record!.RecordedAt,
                    Description = record!.Description,
                    Ledgers = record!.Ledgers!.Select(lg => new ResponseLedger()
                    {
                        AccountId = lg.Account!.AccountCode,
                        Entry = lg.Entry,
                        Balance = lg.Balance
                    }).ToList()
                };
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task<int> ForceDeleteRecordAsync(Guid userId, Guid recordId)
        {
            try
            {
                var record = await _dbContext.Records!.FirstOrDefaultAsync(rc => rc.Id.Equals(recordId) && rc.UserId.Equals(userId));

                _dbContext.Records!.Remove(record!);

                await _dbContext.SaveChangesAsync();
                return 1;
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task<int> RestoreRecordAsync(Guid userId, Guid recordId)
        {
            return await SetDeletedRecordAsync(userId, recordId, null);
        }

        public async Task<int> SoftDeleteRecordAsync(Guid userId, Guid recordId)
        {
            return await SetDeletedRecordAsync(userId, recordId, DateTime.UtcNow);
        }

        private async Task<int> SetDeletedRecordAsync(Guid userId, Guid recordId, DateTime? deletedAt = null)
        {
            try
            {
                var record = await _dbContext.Records!.FirstOrDefaultAsync(rc => rc.Id.Equals(recordId) && rc.UserId.Equals(userId));

                record!.DeletedAt = deletedAt;

                await _dbContext.SaveChangesAsync();
                return 1;
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}
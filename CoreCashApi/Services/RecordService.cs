using System.Security.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreCashApi.Data;
using CoreCashApi.DTOs.Records;
using Microsoft.EntityFrameworkCore;
using CoreCashApi.Entities;

namespace CoreCashApi.Services
{
    public class RecordService
    {
        private readonly AppDbContext _dbContext;

        private readonly ILogger<RecordService> _logger;

        public RecordService(AppDbContext dbContext, ILogger<RecordService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<int> UpdateRecordAsync(Guid userId, Guid recordId, RequestRecordEdit request)
        {
            try
            {
                var record = await _dbContext.Records!
                .Include(rc => rc.Ledgers)
                .FirstOrDefaultAsync(rc => rc.Id.Equals(recordId) && rc.UserId.Equals(userId));

                if (record == default) return 0;

                record!.RecordedAt = request.TransactionDate;
                record!.Description = request.Description;
                List<Ledger> newLedgers = record!.Ledgers!.ToList();
                newLedgers.ForEach(lg =>
                {
                    lg.Entry = request.Entry;
                    lg.Balance = request.Balance;
                });

                record!.Ledgers = newLedgers;

                await _dbContext.SaveChangesAsync();
                return 1;
            }
            catch (System.Exception)
            {
                throw;
            }
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
                record!.UpdatedAt = DateTime.UtcNow;

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
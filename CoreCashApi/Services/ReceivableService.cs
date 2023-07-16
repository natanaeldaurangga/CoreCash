using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreCashApi.Data;
using CoreCashApi.DTOs.Pagination;
using CoreCashApi.DTOs.Records;
using CoreCashApi.Entities;
using CoreCashApi.Enums;
using Microsoft.EntityFrameworkCore;

namespace CoreCashApi.Services
{
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

        public async Task<ResponsePagination<ResponseReceivable>?> GetRecordPagedAsync(Guid userId, RequestPagination request)
        {
            // TODO: Bikin pagination buat receivables, kayaknya bakal banyak join
            
            return null;
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
                var receivable = new Receivable()
                {
                    Id = Guid.NewGuid(),
                    RecordId = record.Id,
                    DebtorId = request.DebtorId,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                await _dbContext.Receivables!.AddAsync(receivable);
                await _dbContext.SaveChangesAsync();
                _logger.LogInformation("end insert receivable");
                // END: Creating New Receivable

                // START: Creating New Receivable Ledger
                _logger.LogInformation("start insert receivable_ledger");
                var recLedger = new ReceivableLedger()
                {
                    ReceivableId = receivable.Id,
                    RecordId = record.Id
                };

                await _dbContext.ReceivableLedgers!.AddAsync(recLedger);
                await _dbContext.SaveChangesAsync();
                _logger.LogInformation("end insert receivable_ledger");
                // END: Creating New Receivable Ledger

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
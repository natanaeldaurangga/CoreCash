using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreCashApi.Data;
using CoreCashApi.DTOs.Records;
using CoreCashApi.Entities;
using CoreCashApi.Utilities;

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

        public async Task<int> InsertNewRecord(Guid userId, RequestCashRecord request)
        {
            using var transaction = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                // var recordType = ;
                var record = new Record()
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    // 
                };
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
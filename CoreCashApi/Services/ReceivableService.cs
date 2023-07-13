using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreCashApi.Data;

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
    }
}
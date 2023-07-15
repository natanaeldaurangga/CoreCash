using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreCashApi.DTOs.Records
{
    public class ResponseCashBalanceSum
    {
        public Guid UserId { get; set; }

        public decimal TotalBalance { get; set; }
    }
}
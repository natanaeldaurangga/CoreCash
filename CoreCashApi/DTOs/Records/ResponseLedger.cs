using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreCashApi.Enums;

namespace CoreCashApi.DTOs.Records
{
    public class ResponseLedger
    {
        public int AccountId { get; set; }

        public Entry Entry { get; set; }

        public decimal Balance { get; set; }
    }
}
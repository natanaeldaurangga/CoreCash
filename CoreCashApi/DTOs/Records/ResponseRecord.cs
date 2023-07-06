using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreCashApi.Enums;

namespace CoreCashApi.DTOs.Records
{
    public class ResponseRecord
    {
        public Guid RecordId { get; set; }

        public DateTime TransactionDate { get; set; }

        public Entry Entry { get; set; }

        public decimal Balance { get; set; }
    }
}
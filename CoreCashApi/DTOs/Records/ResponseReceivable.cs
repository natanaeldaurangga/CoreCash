using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreCashApi.DTOs.Records
{
    public class ResponseReceivable
    {
        public Guid ReceivableId { get; set; }

        public Guid RecordId { get; set; }

        public DateTime TransactionDate { get; set; }

        public Guid DebtorId { get; set; }

        public string DebtorName { get; set; } = string.Empty;

        public decimal Balance { get; set; }
    }
}
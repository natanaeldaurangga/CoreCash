using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CoreCashApi.DTOs.Records
{
    public class ResponsePayable
    {
        public Guid RecordId { get; set; }

        public DateTime TransactionDate { get; set; }

        public Guid CreditorId { get; set; }

        public string CreditorName { get; set; } = string.Empty;

        public string CreditorEmail { get; set; } = string.Empty;

        public decimal Balance { get; set; }
    }
}
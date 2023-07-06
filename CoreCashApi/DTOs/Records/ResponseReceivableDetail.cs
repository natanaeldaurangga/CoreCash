using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreCashApi.DTOs.Records
{
    public class ResponseReceivableDetail
    {
        public Guid ReceivableId { get; set; }

        public Guid DebtorId { get; set; }

        public string? DebtorName { get; set; }

        public string? Description { get; set; }

        public List<ResponseRecord> Records { get; set; } = new();
    }
}
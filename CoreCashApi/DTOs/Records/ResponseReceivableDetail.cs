using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreCashApi.DTOs.Pagination;

namespace CoreCashApi.DTOs.Records
{
    public class ResponseReceivableDetail
    {
        public Guid DebtorId { get; set; }

        public ResponseContact? Debtor { get; set; }

        public ResponsePagination<ResponseRecord>? Records { get; set; }
    }
}
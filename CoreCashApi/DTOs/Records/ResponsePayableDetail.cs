using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreCashApi.DTOs.Pagination;

namespace CoreCashApi.DTOs.Records
{
    public class ResponsePayableDetail
    {
        public Guid CreditorId { get; set; }

        public ResponseContact? Creditor { get; set; }

        public ResponsePagination<ResponseRecord>? Records { get; set; }
    }
}
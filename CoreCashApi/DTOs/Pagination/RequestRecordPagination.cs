using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreCashApi.DTOs.Pagination
{
    public class RequestRecordPagination : RequestPagination
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
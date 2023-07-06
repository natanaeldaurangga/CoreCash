using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreCashApi.DTOs.Pagination
{
    public class RequestPagination
    {
        public int PageSize { get; set; }

        public int CurrentPage { get; set; }

        public string Direction { get; set; } = "ASC";

        public string? SortBy { get; set; } = string.Empty;

        public string? Keyword { get; set; } = string.Empty;
    }
}
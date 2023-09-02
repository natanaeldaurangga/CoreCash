using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreCashApi.DTOs.Pagination
{
    public class ResponsePagination<T>
    {
        public int CurrentPage { get; set; }

        public int PageSize { get; set; }

        public int TotalPages { get; set; }

        public string Direction { get; set; } = "";

        public string SortBy { get; set; } = "";

        public List<T> Items { get; set; } = new List<T>();
    }
}
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreCashApi.Enums;

namespace CoreCashApi.DTOs.Records
{
    public class RequestCashRecord
    {
        [DataType(DataType.DateTime)]
        public DateTime TransactionDate { get; set; }

        [DataType(DataType.Text)]
        public string? Description { get; set; }

        public Entry Entry { get; set; }

        public decimal Balance { get; set; }
    }
}
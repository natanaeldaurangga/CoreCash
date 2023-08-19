using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CoreCashApi.Enums;

namespace CoreCashApi.DTOs.Records
{
    public class RequestRecordEdit
    {
        [DataType(DataType.DateTime)]
        public DateTime TransactionDate { get; set; }

        [DataType(DataType.Text)]
        public string? Description { get; set; }

        [Required]
        [EnumDataType(typeof(Entry))]
        public Entry Entry { get; set; }

        [DataType(DataType.Currency)]
        public decimal Balance { get; set; }
    }
}
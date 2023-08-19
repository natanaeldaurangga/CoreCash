using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CoreCashApi.Validations;

namespace CoreCashApi.DTOs.Records
{
    public class RequestPayableRecord
    {
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime TransactionDate { get; set; }

        [DataType(DataType.Text)]
        public string? Description { get; set; }

        [Required]
        public Guid CreditorId { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Saldo tidak boleh kurang dari 0.")]
        public decimal Balance { get; set; }
    }
}
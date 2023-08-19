using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoreCashApi.DTOs.Records
{
    public class RequestReceivablePayment
    {
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime TransactionDate { get; set; }

        [DataType(DataType.Text)]
        public string? Description { get; set; }

        [Required]
        public Guid DebtorId { get; set; }

        [Required]
        public bool ToCash { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Saldo tidak boleh kurang dari 0.")]
        public decimal Balance { get; set; }
    }
}
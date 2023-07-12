using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CoreCashApi.Entities
{
    [Table("payables")]
    [Index(nameof(RecordId), nameof(DebtorId))]
    public class Payable : BaseEntity
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        [Required]
        [Column("record_id")]
        public Guid RecordId { get; set; }

        public Record? Record { get; set; }

        [Required]
        [Column("debtor_id")]
        public Guid DebtorId { get; set; }

        public Contact? Debtor { get; set; }

        public ICollection<PayableLedger>? PayableLedgers { get; set; }
    }
}
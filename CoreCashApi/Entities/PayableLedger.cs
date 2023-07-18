using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using CoreCashApi.Enums;
using Microsoft.EntityFrameworkCore;

namespace CoreCashApi.Entities
{
    [Table("payable_ledgers")]
    [Index(nameof(RecordId), nameof(CreditorId))]
    public class PayableLedger : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public Guid Id { get; set; }

        [Required]
        [Column("record_id")]
        public Guid RecordId { get; set; }

        public Record? Record { get; set; }

        [Required]
        [Column("record_group")]
        public RecordGroup RecordGroup { get; set; }

        [Required]
        [Column("creditor_id")]
        public Guid CreditorId { get; set; }

        public Contact? Creditor { get; set; }
    }
}
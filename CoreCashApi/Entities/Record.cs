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
    [Table("records")]
    [Index(nameof(UserId))]
    public class Record : BaseEntity
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        [Required]
        [Column("user_id")]
        public Guid UserId { get; set; }

        public User? User { get; set; }

        [Required]
        [Column("record_group")]
        public RecordGroup RecordGroup { get; set; }

        [DataType(DataType.DateTime)]
        [Column("recorded_at")]
        public DateTime RecordedAt { get; set; }

        [DataType(DataType.Text)]
        [Column("description")]
        public string? Description { get; set; }

        public ICollection<Ledger>? Ledgers { get; set; }

        public Receivable? Receivable { get; set; }

        public ReceivableLedger? ReceivableLedger { get; set; }

        public Payable? Payable { get; set; }

        public PayableLedger? PayableLedger { get; set; }
    }
}
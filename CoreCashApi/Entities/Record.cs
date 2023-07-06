using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CoreCashApi.Entities
{
    [Table("records")]
    public class Record : BaseEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid UserId { get; set; }

        public User? User { get; set; }

        [Required]
        [Column("record_type_id")]
        public Guid RecordTypeId { get; set; }

        public RecordType? RecordType { get; set; }

        [DataType(DataType.DateTime)]
        [Column("recorded_at")]
        public DateTime RecordedAt { get; set; }

        [DataType(DataType.Text)]
        [Column("description")]
        public string? Description { get; set; }

        public ICollection<JournalEntry>? JournalEntries { get; set; }

        public Receivable? Receivable { get; set; }

        public ReceivableLedger? ReceivableLedger { get; set; }

        public Payable? Payable { get; set; }

        public PayableLedger? PayableLedger { get; set; }
    }
}
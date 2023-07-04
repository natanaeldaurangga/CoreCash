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

        [Required]
        [Column("record_type_id")]
        public int RecordTypeId { get; set; }

        public RecordType? RecordType { get; set; }

        [DataType(DataType.DateTime)]
        [Column("recorded_at")]
        public DateTime RecordedAt { get; set; }

        [DataType(DataType.Text)]
        [Column("description")]
        public string? Description { get; set; }

        public ICollection<JournalEntry>? JournalEntries { get; set; }

        public ICollection<Receivable>? Receivables { get; set; }

        public ICollection<Payable>? Payables { get; set; }
    }
}
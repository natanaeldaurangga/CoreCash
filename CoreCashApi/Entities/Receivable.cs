using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CoreCashApi.Entities
{
    [Table("receivables")]
    [Index(nameof(RecordId), nameof(CreditorId))]
    public class Receivable : BaseEntity
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        [Required]
        [Column("record_id")]
        public Guid RecordId { get; set; }

        public Record? Record { get; set; }

        [Required]
        [Column("creditor_id")]
        public Guid CreditorId { get; set; }

        public Contact? Creditor { get; set; }

        public ICollection<ReceivableLedger>? ReceivableLedgers { get; set; }
    }
}
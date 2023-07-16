using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CoreCashApi.Entities
{
    [Table("receivables")]
    [Index(nameof(RecordId), nameof(DebtorId))]
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
        [Column("debtor_id")]
        public Guid DebtorId { get; set; }

        public Contact? Debtor { get; set; }

        public ICollection<ReceivableLedger>? ReceivableLedgers { get; set; }
    }
}
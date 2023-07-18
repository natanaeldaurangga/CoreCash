using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CoreCashApi.Entities
{
    [Table("receivable_ledgers")]
    [Index(nameof(RecordId), nameof(DebtorId))]
    public class ReceivableLedger : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("record_id")]
        public Guid RecordId { get; set; }

        public Record? Record { get; set; }

        [Required]
        [Column("debtor_id")]
        public Guid DebtorId { get; set; }

        public Contact? Debtor { get; set; }
    }
}
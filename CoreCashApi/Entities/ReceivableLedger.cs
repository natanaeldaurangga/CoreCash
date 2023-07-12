using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CoreCashApi.Entities
{
    [Table("receivable_ledger")]
    [Index(nameof(RecordId), nameof(ReceivableId))]
    public class ReceivableLedger
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public Guid RecordId { get; set; }

        public Record? Record { get; set; }

        [Required]
        public Guid ReceivableId { get; set; }

        public Receivable? Receivable { get; set; }
    }
}
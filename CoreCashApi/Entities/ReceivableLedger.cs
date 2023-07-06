using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreCashApi.Entities
{
    [Table("receivable_ledger")]
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
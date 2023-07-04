using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CoreCashApi.Entities
{
    [Table("payables")]
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
    }
}
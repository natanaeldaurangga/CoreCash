using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CoreCashApi.Entities
{
    [Table("payable_ledger")]
    [Index(nameof(RecordId), nameof(PayableId))]
    public class PayableLedger
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public Guid RecordId { get; set; }

        public Record? Record { get; set; }

        [Required]
        public Guid PayableId { get; set; }

        public Payable? Payable { get; set; }
    }
}
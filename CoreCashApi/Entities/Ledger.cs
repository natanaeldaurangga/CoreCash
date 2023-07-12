using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CoreCashApi.Enums;
using Microsoft.EntityFrameworkCore;

namespace CoreCashApi.Entities
{
    [Table("ledgers")]
    [Index(nameof(RecordId), nameof(AccountId))]
    public class Ledger
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        [Required]
        [Column("record_id")]
        public Guid RecordId { get; set; }

        public Record? Record { get; set; }

        [Required]
        [Column("account_id")]
        public Guid AccountId { get; set; }

        public Account? Account { get; set; }

        [Required]
        [Column("entry")]
        public Entry Entry { get; set; } = Entry.CREDIT;

        [DataType(DataType.Currency)]
        [DefaultValue(0)]
        [Column("balance")]
        public decimal Balance { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using CoreCashApi.Enums;

namespace CoreCashApi.Entities
{
    [Table("accounts")]
    public class Account : BaseEntity
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        [Required]
        [Column("account_code")]
        public int AccountCode { get; set; }

        [Required]
        [Column("account_group")]
        public AccountGroup AccountGroup { get; set; }

        [Required]
        [Column("account_name")]
        public string AccountName { get; set; } = string.Empty;

        public ICollection<Ledger>? Ledgers { get; set; }
    }
}
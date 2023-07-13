using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CoreCashApi.Entities
{
    [Table("contacts")]
    [Index(nameof(Name))]
    public class Contact : BaseEntity
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        [Required]
        [Column("user_id")]
        public Guid UserId { get; set; }

        public User? User { get; set; }

        [Required]
        [MaxLength(255)]
        [Column("name")]
        public string Name { get; set; } = string.Empty;

        [MaxLength(12)]
        [DataType(DataType.PhoneNumber)]
        [Column("phone_number")]
        public string PhoneNumber { get; set; } = string.Empty;

        [DataType(DataType.Text)]
        [Column("address")]
        public string Address { get; set; } = string.Empty;

        public ICollection<Receivable>? Receivables { get; set; }

        public ICollection<Payable>? Payables { get; set; }

    }
}
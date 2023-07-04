using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CoreCashApi.Entities
{
    [Table("roles")]
    public class Role : BaseEntity
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        [MaxLength(50)]
        [Required]
        [Column("name")]
        public string Name { get; set; } = string.Empty;

        [JsonIgnore]
        public ICollection<User>? Users { get; set; }
    }
}
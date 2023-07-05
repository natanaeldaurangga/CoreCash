using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreCashApi.Entities
{
    [Table("record_types")]
    public class RecordType : BaseEntity
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        [Required]
        [Column("name")]
        public string Name { get; set; } = string.Empty;

        public ICollection<Record>? Records { get; set; }
    }
}
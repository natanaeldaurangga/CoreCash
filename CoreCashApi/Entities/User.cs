using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CoreCashApi.Entities
{
    [Table("users")]
    // [Index(nameof(User.))]
    public class User : BaseEntity
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        [Column("name")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.EmailAddress)]
        [MaxLength(255)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [Column("password_hash")]
        public byte[] PasswordHash { get; set; } = Array.Empty<byte>();

        [Required]
        [Column("password_salt")]
        public byte[] PasswordSalt { get; set; } = Array.Empty<byte>();

        [Column("profile_picture")]
        [DefaultValue(null)]
        public string? ProfilePicture { get; set; }

        [MaxLength(255)]
        [Column("refresh_token")]
        public string RefreshToken { get; set; } = string.Empty;

        [DataType(DataType.DateTime)]
        [Column("token_created")]
        public DateTime TokenCreated { get; set; }

        [DataType(DataType.DateTime)]
        [Column("token_expires")]
        public DateTime TokenExpires { get; set; }

        [Required]
        [Column("role_id")]
        public Guid RoleId { get; set; }

        public Role? Role { get; set; }

        public Record? Record { get; set; }
    }
}
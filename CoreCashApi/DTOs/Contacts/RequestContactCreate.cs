using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoreCashApi.DTOs.Contacts
{
    public class RequestContactCreate
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [DataType(DataType.PhoneNumber)]
        [MaxLength(14, ErrorMessage = "Maksimal jumlah karakter untuk nomor telepon adalah 14 karakter.")]
        public string? PhoneNumber { get; set; }

        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [MaxLength(255, ErrorMessage = "Maksimal jumlah karakter untuk alamat adalah 255 karakter.")]
        public string? Address { get; set; }
    }
}
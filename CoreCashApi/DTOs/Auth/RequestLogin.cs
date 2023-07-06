using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoreCashApi.DTOs.Auth
{
    public class RequestLogin
    {
        [MaxLength(255, ErrorMessage = "Field 'email' tidak boleh lebih dari 255 karakter.")]
        [EmailAddress(ErrorMessage = "Field 'Email' tidak valid.")]
        [Required]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}
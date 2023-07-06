using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoreCashApi.DTOs.Auth
{
    public class ResetPasswordRequest
    {
        [Required(ErrorMessage = "Field 'email' wajib diisi.")]
        public string Email { get; set; } = string.Empty;
    }
}
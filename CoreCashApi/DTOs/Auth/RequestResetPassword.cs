using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CoreCashApi.Validations;

namespace CoreCashApi.DTOs.Auth
{
    public class RequestResetPassword
    {
        [Required(ErrorMessage = "Field 'email' wajib diisi.")]
        [EmailUnique(MustUnique = false, ErrorMessage = "Email yang anda inputkan belum terdaftar.")]
        public string Email { get; set; } = string.Empty;
    }
}
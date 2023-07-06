using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoreCashApi.DTOs.Auth
{
    public class NewPasswordRequest
    {
        [MaxLength(255, ErrorMessage = "Field 'password' tidak boleh lebih dari 255 karakter.")]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$", ErrorMessage = "Password harus memiliki 8 karakter serta mengandung karakter unik, angka, huruf kapital, dan huruf kecil.")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Compare(nameof(Password), ErrorMessage = "Field Konfirmasi password dengan password tidak sama.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; } = string.Empty;

    }
}
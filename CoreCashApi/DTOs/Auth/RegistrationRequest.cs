using System.Text.RegularExpressions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoreCashApi.DTOs.Auth
{
    public class RegistrationRequest
    {
        [MaxLength(255, ErrorMessage = "Field 'Nama lengkap' tidak boleh lebih dari 255 karakter.")]
        [Required(ErrorMessage = "Field 'Nama lengkap' wajib diisi.")]
        public string? FullName { get; set; }

        [MaxLength(255, ErrorMessage = "Field 'email' tidak boleh lebih dari 255 karakter.")]
        [EmailAddress(ErrorMessage = "Field 'Email' tidak valid.")]
        [Required(ErrorMessage = "Field 'Email' wajib diisi.")]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$", ErrorMessage = "Password harus memiliki 8 karakter serta mengandung karakter unik, angka, huruf kapital, dan huruf kecil.")]
        public string? Email { get; set; }

        [MaxLength(255, ErrorMessage = "Field 'password' tidak boleh lebih dari 255 karakter.")]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$", ErrorMessage = "Password harus memiliki 8 karakter serta mengandung karakter unik, angka, huruf kapital, dan huruf kecil.")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Compare(nameof(Password), ErrorMessage = "Field Konfirmasi password dengan password tidak sama.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
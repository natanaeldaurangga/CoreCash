using System.Text.RegularExpressions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoreCashApi.Validations;

namespace CoreCashApi.DTOs.Auth
{
    public class RequestRegistration
    {
        [MaxLength(255, ErrorMessage = "Field 'Nama lengkap' tidak boleh lebih dari 255 karakter.")]
        [Required(ErrorMessage = "Field 'Nama lengkap' wajib diisi.")]
        public string FullName { get; set; } = string.Empty;

        [MaxLength(255, ErrorMessage = "Field 'email' tidak boleh lebih dari 255 karakter.")]
        [EmailAddress(ErrorMessage = "Field 'Email' tidak valid.")]
        [Required(ErrorMessage = "Field 'Email' wajib diisi.")]
        [EmailUnique(ErrorMessage = "Email sudah digunakan")]
        public string Email { get; set; } = string.Empty;

        [MaxLength(255, ErrorMessage = "Field 'password' tidak boleh lebih dari 255 karakter.")]
        // [AppRegex(RegexPattern = "^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$", ErrorMessage = "Password harus memiliki 8 karakter serta mengandung karakter unik, angka, huruf kapital, dan huruf kecil.")]
        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; } = string.Empty;

        [Compare(nameof(RequestRegistration.Password), ErrorMessage = "Field Konfirmasi password dengan password tidak sama.")]
        [DataType(DataType.Password)]
        [Required]
        public string ConfirmPassword { get; set; } = string.Empty;

        [FromForm(Name = "Image")]
        [AppFileExtensions(AllowMimeTypes = new string[] { "image/png", "image/jpeg" }, ErrorMessage = "Ekstensi yang didukung hanya jpeg dan png.")]
        [AppFileSize(3 * 1024 * 1024, ErrorMessage = "Maksimal ukuran file adalah 3 MB.")]
        public IFormFile? ProfilePicture { get; set; }
    }
}
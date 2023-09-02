using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using CoreCashApi.Enums;

namespace CoreCashApi.DTOs.Auth
{
    public class ResponseLogin
    {
        public Guid UserId { get; set; }

        public string? FullName { get; set; }

        public string? Email { get; set; }

        public string? Role { get; set; }

        public string? JwtToken { get; set; }

        public string? ProfilePicture { get; set; }

        [JsonIgnore]
        public AuthError? Error { get; set; }
    }
}
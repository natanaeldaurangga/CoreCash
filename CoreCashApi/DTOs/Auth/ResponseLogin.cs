using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreCashApi.DTOs.Auth
{
    public class ResponseLogin
    {
        public string? FullName { get; set; }

        public string? Email { get; set; }

        public string? Role { get; set; }

        public string? JwtToken { get; set; }
    }
}
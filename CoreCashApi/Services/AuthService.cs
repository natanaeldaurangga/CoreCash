using System.Security.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreCashApi.Data;
using System.Text;
using CoreCashApi.Entities;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace CoreCashApi.Services
{
    public class AuthService
    {
        protected readonly AppDbContext _context;

        protected readonly IConfiguration _config;

        public AuthService(AppDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using var hmac = new HMACSHA256();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }

        private static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using var hmac = new HMACSHA256(passwordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(passwordHash);
        }

        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config.GetSection("Jwt:Key").Value);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Audience = _config.GetSection("Jwt:Audience").Value,
                Issuer = _config.GetSection("Jwt:Issuer").Value
            };

            var emailClaim = new Claim(ClaimTypes.Email, user.Email!);
            var roleClaim = new Claim(ClaimTypes.Role, user.Role!.Name!);
            var idClaim = new Claim("id", user.Id.ToString());
            var nameClaim = new Claim(ClaimTypes.Name, user.FullName ?? "");

            tokenDescriptor.Subject = new ClaimsIdentity(new Claim[]{
                idClaim, nameClaim, emailClaim, roleClaim
            });

            tokenDescriptor.Expires = DateTime.Now.AddHours(6);

            tokenDescriptor.SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        private static string CreateRandomToken()
        {
            return Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
        }
    }
}
using System.Collections;
using System.Net.Mime;
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
using CoreCashApi.DTOs.Auth;
using Microsoft.EntityFrameworkCore;
using CoreCashApi.Enums;
using CoreCashApi.Utilities;

namespace CoreCashApi.Services
{
    public class AuthService
    {
        protected readonly AppDbContext _dbContext;

        protected readonly IConfiguration _config;

        protected readonly ImageUtility _imageUtil;

        public AuthService(AppDbContext context, IConfiguration config, ImageUtility imageUtil)
        {
            _dbContext = context;
            _config = config;
            _imageUtil = imageUtil;
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

        private static string GenerateRandomToken()
        {
            return Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
        }

        public async Task<ResponseLogin?> LoginAsync(RequestLogin request)
        {
            var user = await _dbContext.Users!
            .Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.Email.Equals(request.Email));

            if (user == null) return new ResponseLogin { Error = AuthError.NOT_FOUND };

            if (user!.DeletedAt != null) return new ResponseLogin { Error = AuthError.INACTIVE };

            if (user!.VerifiedAt == default) return new ResponseLogin { Error = AuthError.UNVERIFIED };

            string token = GenerateJwtToken(user);

            return new ResponseLogin
            {
                UserId = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Role = user.Role!.Name,
                JwtToken = token
            };
        }

        public async Task<string> RegisterAsync(RequestRegistration request)
        {
            var imageName = "";
            if (request.ProfilePicture != null)
                imageName = await _imageUtil.UploadImageAsync(request.ProfilePicture, folder: "uploads");
            try
            {
                CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

                var role = await _dbContext.Roles!.FirstOrDefaultAsync(r => r.Name!.Equals("ROLE_USER"));

                var user = new User
                {
                    Id = Guid.NewGuid(),
                    FullName = request.FullName,
                    Email = request.Email,
                    RoleId = role!.Id,
                    Role = role,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    ProfilePicture = imageName,
                    VerificationToken = GenerateRandomToken(),
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    TokenExpires = DateTime.UtcNow.AddHours(24)
                };

                await _dbContext.Users!.AddAsync(user);
                await _dbContext.SaveChangesAsync();
                return user.VerificationToken;
            }
            catch (Exception)
            {
                _imageUtil.DeleteImage(imageName);
                throw;
            }
        }

        public async Task<bool> VerifyUserAsync(string token)
        {
            var user = await _dbContext.Users!.FirstOrDefaultAsync(u => u.VerificationToken!.Equals(token));

            user!.VerificationToken = null;
            user!.VerifiedAt = DateTime.UtcNow;

            await _dbContext.SaveChangesAsync();

            return user != default;
        }

        public IEnumerable<int> TestYield()
        {
            for (int i = 0; i < 10; i++)
            {
                yield return i;
            }
        }
    }
}
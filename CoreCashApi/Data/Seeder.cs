using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreCashApi.Entities;
using CoreCashApi.Enums;
using CoreCashApi.Services;
using Microsoft.EntityFrameworkCore;

namespace CoreCashApi.Data
{
    public static class Seeder
    {
        public static void Run(ModelBuilder modelBuilder)
        {
            #region SEED ROLE
            // SEED ROLE
            var adminRole = new Role { Id = Guid.NewGuid(), Name = "ROLE_ADMIN" };
            var userRole = new Role { Id = Guid.NewGuid(), Name = "ROLE_USER" };

            modelBuilder.Entity<Role>()
            .HasData(adminRole, userRole);
            #endregion

            #region SEED USER
            // BIKIN PASSWORD
            AuthService.CreatePasswordHash("Tester1234", out byte[] passwordHash, out byte[] passwordSalt);

            // SEED USER
            var admin1 = new User()
            {
                Id = Guid.NewGuid(),
                Email = "admin1@example.com",
                FullName = "Admin Satu",
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                RoleId = adminRole.Id,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            var admin2 = new User()
            {
                Id = Guid.NewGuid(),
                Email = "admin2@example.com",
                FullName = "Admin Dua",
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                RoleId = adminRole.Id,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            var user1 = new User()
            {
                Id = Guid.NewGuid(),
                Email = "user1@example.com",
                FullName = "User Satu",
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                RoleId = userRole.Id,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };

            var user2 = new User()
            {
                Id = Guid.NewGuid(),
                Email = "user2@example.com",
                FullName = "User Dua",
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                RoleId = userRole.Id,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };

            modelBuilder.Entity<User>()
            .HasData(admin1, admin2, user1, user2);
            #endregion

            #region SEED ACCOUNT
            modelBuilder.Entity<Account>()
            .HasData(
                new Account { Id = Guid.NewGuid(), AccountCode = AccountCodes.CASH, AccountGroup = AccountGroup.CurrentAssets, AccountName = "CASH" },
                new Account { Id = Guid.NewGuid(), AccountCode = AccountCodes.RECEIVABLE, AccountGroup = AccountGroup.CurrentAssets, AccountName = "RECEIVABLE" },
                new Account { Id = Guid.NewGuid(), AccountCode = AccountCodes.PAYABLE, AccountGroup = AccountGroup.CurrentLiabilities, AccountName = "PAYABLE" }
            );
            #endregion

            #region SEED TRANSACTION
            var records = new List<Record>()
            {
                new Record {
                    Id = Guid.NewGuid(),
                    
                }
            }
            #endregion
        }
    }
}
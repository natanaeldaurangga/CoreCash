using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreCashApi.Data.SeedEntity;
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
                VerifiedAt = DateTime.UtcNow,
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
                VerifiedAt = DateTime.UtcNow,
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
                VerifiedAt = DateTime.UtcNow,
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
                VerifiedAt = DateTime.UtcNow,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };

            modelBuilder.Entity<User>()
            .HasData(admin1, admin2, user1, user2);
            #endregion

            #region SEED ACCOUNT
            var cashAccount = new Account { Id = Guid.NewGuid(), AccountCode = AccountCodes.CASH, AccountGroup = AccountGroup.CurrentAssets, AccountName = "CASH" };

            var receivableAccount = new Account { Id = Guid.NewGuid(), AccountCode = AccountCodes.RECEIVABLE, AccountGroup = AccountGroup.CurrentAssets, AccountName = "RECEIVABLE" };

            var payableAccount = new Account { Id = Guid.NewGuid(), AccountCode = AccountCodes.PAYABLE, AccountGroup = AccountGroup.CurrentLiabilities, AccountName = "PAYABLE" };

            modelBuilder.Entity<Account>()
            .HasData(
                cashAccount,
                receivableAccount,
                payableAccount
            );
            #endregion

            #region SEED TRANSACTION

            // User 1
            var user1Transaction = GenerateRandomCashTransaction(20, user1.Id, cashAccount);
            modelBuilder.Entity<Record>().HasData(user1Transaction!.Records!);
            modelBuilder.Entity<Ledger>().HasData(user1Transaction!.Ledgers!);

            // User2
            var user2Transaction = GenerateRandomCashTransaction(20, user2.Id, cashAccount);
            modelBuilder.Entity<Record>().HasData(user2Transaction!.Records!);
            modelBuilder.Entity<Ledger>().HasData(user2Transaction!.Ledgers!);

            #endregion

            #region SEED CONTACTS

            modelBuilder.Entity<Contact>()
            .HasData(
                new Contact { Id = Guid.NewGuid(), UserId = user1.Id, Name = "Randell Havock", PhoneNumber = "6765109874", Email = "rhavock0@gov.uk", Address = "620 Lakeland Center" },
                new Contact { Id = Guid.NewGuid(), UserId = user1.Id, Name = "Sarine Mahood", PhoneNumber = "6977095403", Email = "smahood1@wufoo.com", Address = "34 Eastwood Avenue" },
                new Contact { Id = Guid.NewGuid(), UserId = user1.Id, Name = "Benjie McGing", PhoneNumber = "5025308916", Email = "bmcging2@ihg.com", Address = "17 Onsgard Avenue" },
                new Contact { Id = Guid.NewGuid(), UserId = user1.Id, Name = "Kev Lanphere", PhoneNumber = "7268834724", Email = "klanphere3@upenn.edu", Address = "96 Summerview Drive" },
                new Contact { Id = Guid.NewGuid(), UserId = user1.Id, Name = "Sal Blanchard", PhoneNumber = "4904310249", Email = "sblanchard4@vinaora.com", Address = "39494 Kartens Terrace" },
                new Contact { Id = Guid.NewGuid(), UserId = user1.Id, Name = "Laurette Dunster", PhoneNumber = "7678939672", Email = "ldunster5@marketwatch.com", Address = "615 Jenifer Alley" },
                new Contact { Id = Guid.NewGuid(), UserId = user2.Id, Name = "Tiphany Shew", PhoneNumber = "1457940882", Email = "tshew6@tripadvisor.com", Address = "4649 Swallow Trail" },
                new Contact { Id = Guid.NewGuid(), UserId = user2.Id, Name = "Ceil Andrelli", PhoneNumber = "1401949061", Email = "candrelli7@intel.com", Address = "10 Calypso Center" },
                new Contact { Id = Guid.NewGuid(), UserId = user2.Id, Name = "Margaretta Sivorn", PhoneNumber = "5311644464", Email = "msivorn8@icq.com", Address = "11 Old Shore Terrace" },
                new Contact { Id = Guid.NewGuid(), UserId = user2.Id, Name = "Vasilis Woodsford", PhoneNumber = "8364764344", Email = "vwoodsford9@issuu.com", Address = "4330 Doe Crossing Junction" },
                new Contact { Id = Guid.NewGuid(), UserId = user2.Id, Name = "Jennee Orneblow", PhoneNumber = "9316403125", Email = "jorneblowa@youtube.com", Address = "54 Redwing Drive" }
            );

            #endregion
        }

        public static CashSeed? GenerateRandomCashTransaction(int length, Guid userId, Account cashAccount)
        {
            if (length == 0) return null;
            var records = new Record[length];
            var ledgers = new Ledger[length];

            var entries = new Entry[] { Entry.DEBIT, Entry.CREDIT };
            var rcGroup = new RecordGroup[] { RecordGroup.CASH_IN, RecordGroup.CASH_OUT };
            int randIndex;
            // var rand = new Random();
            for (int i = 0; i < length; i++)
            {
                randIndex = (i + 1) % 2;
                records[i] = new Record()
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    RecordGroup = rcGroup[randIndex],
                    RecordedAt = DateTime.Now,
                    Description = "Lorem ipsum dolor sit amet.",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                decimal balance = (decimal)((length + 1 - i) * 10000);

                ledgers[i] = new Ledger()
                {
                    Id = Guid.NewGuid(),
                    AccountId = cashAccount.Id,
                    Entry = entries[randIndex],
                    Balance = balance,
                    RecordId = records[i].Id
                };
            }

            return new CashSeed
            {
                Records = records,
                Ledgers = ledgers
            };
        }
    }
}
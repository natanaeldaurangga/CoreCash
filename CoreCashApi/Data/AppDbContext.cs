using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreCashApi.Entities;
using CoreCashApi.Enums;
using Microsoft.EntityFrameworkCore;

namespace CoreCashApi.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User>? Users { get; set; }

        public DbSet<Role>? Roles { get; set; }

        public DbSet<Record>? Records { get; set; }

        public DbSet<Account>? Accounts { get; set; }

        public DbSet<Ledger>? Ledgers { get; set; }

        public DbSet<Contact>? Contacts { get; set; }

        public DbSet<Receivable>? Receivables { get; set; }

        public DbSet<Payable>? Payables { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseIdentityColumns();

            #region Constraint Relation
            modelBuilder.Entity<User>()
            .HasOne(u => u.Role)
            .WithMany(r => r.Users)
            .HasForeignKey(u => u.RoleId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Record>()
            .HasOne(rc => rc.User)
            .WithMany(u => u.Records)
            .HasForeignKey(rc => rc.UserId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Ledger>()
            .HasOne(je => je.Account)
            .WithMany(acc => acc.Ledgers)
            .HasForeignKey(je => je.AccountId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Ledger>()
            .HasOne(je => je.Record)
            .WithMany(rc => rc.Ledgers)
            .HasForeignKey(je => je.RecordId)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Receivable>()
            .HasOne(rcv => rcv.Creditor)
            .WithMany(crt => crt.Receivables)
            .HasForeignKey(rcv => rcv.CreditorId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Receivable>()
            .HasOne(rcv => rcv.Record)
            .WithOne(rc => rc.Receivable)
            .HasForeignKey<Receivable>(rcv => rcv.RecordId)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ReceivableLedger>()
            .HasOne(rl => rl.Record)
            .WithOne(rc => rc.ReceivableLedger)
            .HasForeignKey<ReceivableLedger>(rcv => rcv.ReceivableId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ReceivableLedger>()
            .HasOne(rl => rl.Receivable)
            .WithMany(rcv => rcv.ReceivableLedgers)
            .HasForeignKey(rl => rl.ReceivableId);

            modelBuilder.Entity<Payable>()
            .HasOne(pyb => pyb.Debtor)
            .WithMany(dbt => dbt.Payables)
            .HasForeignKey(pyb => pyb.DebtorId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Payable>()
            .HasOne(pyb => pyb.Record)
            .WithOne(rc => rc.Payable)
            .HasForeignKey<Payable>(pyb => pyb.RecordId)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PayableLedger>()
            .HasOne(pl => pl.Record)
            .WithOne(rc => rc.PayableLedger)
            .HasForeignKey<PayableLedger>(pl => pl.RecordId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PayableLedger>()
            .HasOne(pl => pl.Payable)
            .WithMany(pyb => pyb.PayableLedgers)
            .HasForeignKey(pyb => pyb.PayableId);

            #endregion

            #region Seeder
            var adminRole = new Role { Id = Guid.NewGuid(), Name = "ROLE_ADMIN" };
            var userRole = new Role { Id = Guid.NewGuid(), Name = "ROLE_USER" };

            modelBuilder.Entity<Role>()
            .HasData(adminRole, userRole);


            modelBuilder.Entity<Account>()
            .HasData(
                new Account { Id = Guid.NewGuid(), AccountCode = AccountCodes.CASH, AccountGroup = AccountGroup.CurrentAssets, AccountName = "CASH" },
                new Account { Id = Guid.NewGuid(), AccountCode = AccountCodes.RECEIVABLE, AccountGroup = AccountGroup.CurrentAssets, AccountName = "RECEIVABLE" },
                new Account { Id = Guid.NewGuid(), AccountCode = AccountCodes.PAYABLE, AccountGroup = AccountGroup.CurrentLiabilities, AccountName = "PAYABLE" }
            );
            #endregion
        }

        public Account? Cash => Accounts!.FirstOrDefault(acc => acc.AccountName.Equals("Cash"));
    }
}
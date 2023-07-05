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

        public DbSet<RecordType>? RecordTypes { get; set; }

        public DbSet<Record>? Records { get; set; }

        public DbSet<Account>? Accounts { get; set; }

        public DbSet<JournalEntry>? JournalEntries { get; set; }

        public DbSet<Contact>? Contacts { get; set; }

        public DbSet<Receivable>? Receivables { get; set; }

        public DbSet<Payable>? Payables { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseIdentityColumns();

            modelBuilder.Entity<User>()
            .HasOne(u => u.Role)
            .WithMany(r => r.Users)
            .HasForeignKey(u => u.RoleId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Record>()
            .HasOne(rc => rc.RecordType)
            .WithMany(rct => rct.Records)
            .HasForeignKey(rc => rc.RecordTypeId)
            .OnDelete(DeleteBehavior.Restrict);

            // TODO: Lanjut bikin relasi antar tabel
            modelBuilder.Entity<Record>()
            .HasOne(rc => rc.User)
            .WithMany(u => u.Records)
            .HasForeignKey(rc => rc.UserId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<JournalEntry>()
            .HasOne(je => je.Account)
            .WithMany(acc => acc.JournalEntries)
            .HasForeignKey(je => je.AccountId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<JournalEntry>()
            .HasOne(je => je.Record)
            .WithMany(rc => rc.JournalEntries)
            .HasForeignKey(je => je.RecordId)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Receivable>()
            .HasOne(rcv => rcv.Creditor)
            .WithMany(crt => crt.Receivables)
            .HasForeignKey(rcv => rcv.CreditorId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Receivable>()
            .HasOne(rcv => rcv.Record)
            .WithMany(rc => rc.Receivables)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Payable>()
            .HasOne(pyb => pyb.Debtor)
            .WithMany(dbt => dbt.Payables)
            .HasForeignKey(pyb => pyb.DebtorId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Payable>()
            .HasOne(pyb => pyb.Record)
            .WithMany(rc => rc.Payables)
            .HasForeignKey(pyb => pyb.RecordId)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RecordType>()
            .HasData(
                new RecordType { Id = Guid.NewGuid(), Name = "CASH_IN" },
                new RecordType { Id = Guid.NewGuid(), Name = "CASH_OUT" },
                new RecordType { Id = Guid.NewGuid(), Name = "NEW_RECEIVABLE" },
                new RecordType { Id = Guid.NewGuid(), Name = "NEW_PAYABLE" },
                new RecordType { Id = Guid.NewGuid(), Name = "RECEIVABLE_PAYMENT" },
                new RecordType { Id = Guid.NewGuid(), Name = "PAYABLE_PAYMENT" }
            );

            modelBuilder.Entity<Account>()
            .HasData(
                new Account { Id = Guid.NewGuid(), AccountCode = "11001", AccountGroup = AccountGroup.CurrentAssets, AccountName = "CASH" },
                new Account { Id = Guid.NewGuid(), AccountCode = "11005", AccountGroup = AccountGroup.CurrentAssets, AccountName = "RECEIVABLE" },
                new Account { Id = Guid.NewGuid(), AccountCode = "21001", AccountGroup = AccountGroup.CurrentLiabilities, AccountName = "PAYABLE" }
            );

            // base.OnModelCreating(modelBuilder);
        }

    }
}
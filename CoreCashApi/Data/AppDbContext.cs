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
    public class AppDbContext : DbContext
    {
        public DbSet<User>? Users { get; set; }

        public DbSet<Role>? Roles { get; set; }

        public DbSet<Record>? Records { get; set; }

        public DbSet<Account>? Accounts { get; set; }

        public DbSet<Ledger>? Ledgers { get; set; }

        public DbSet<Contact>? Contacts { get; set; }

        public DbSet<ReceivableLedger>? ReceivableLedgers { get; set; }

        public DbSet<PayableLedger>? PayableLedgers { get; set; }

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

            modelBuilder.Entity<Contact>()
            .HasOne(ct => ct.User)
            .WithMany(u => u.Contacts)
            .HasForeignKey(ct => ct.UserId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ReceivableLedger>()
            .HasOne(rl => rl.Record)
            .WithOne(rc => rc.ReceivableLedger)
            .HasForeignKey<ReceivableLedger>(rl => rl.RecordId)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ReceivableLedger>()
            .HasOne(rl => rl.Debtor)
            .WithMany(ct => ct.ReceivableLedgers)
            .HasForeignKey(rl => rl.DebtorId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PayableLedger>()
            .HasOne(pl => pl.Record)
            .WithOne(rc => rc.PayableLedger)
            .HasForeignKey<PayableLedger>(pl => pl.RecordId)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PayableLedger>()
            .HasOne(pl => pl.Creditor)
            .WithMany(ct => ct.PayableLedgers)
            .HasForeignKey(pl => pl.CreditorId)
            .OnDelete(DeleteBehavior.Restrict);
            #endregion

            #region seeding
            Seeder.Run(modelBuilder);
            #endregion
        }
    }
}
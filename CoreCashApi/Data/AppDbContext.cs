using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreCashApi.Entities;
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
            // modelBuilder.Entity<

            base.OnModelCreating(modelBuilder);
        }

    }
}
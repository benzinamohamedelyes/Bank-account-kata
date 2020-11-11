using BankAccount.Domain;
using Microsoft.EntityFrameworkCore;
using System;

namespace BankAccount.Data
{
    public class BankAccountContext: DbContext
    {
        public BankAccountContext()
        {

        }
        public BankAccountContext(DbContextOptions options): base(options)
        {

        }
        public DbSet<Bank> Banks { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne(a => a.Account)
                .WithOne(b => b.Owner)
                .HasForeignKey<Account>(b => b.UserId);
        }
    }
}

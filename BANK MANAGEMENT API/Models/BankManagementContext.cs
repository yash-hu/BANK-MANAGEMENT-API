using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BANK_MANAGEMENT_API.Models
{
    public partial class BankManagementContext : DbContext
    {
        public BankManagementContext()
        {
        }

        public BankManagementContext(DbContextOptions<BankManagementContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; } = null!;
        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<Interest> Interests { get; set; } = null!;
        public virtual DbSet<Transaction> Transactions { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=dbconnection");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(e => e.AccountNo)
                    .HasName("PK__ACCOUNTS__46A52636BB8B7402");

                entity.ToTable("ACCOUNTS");

                entity.HasIndex(e => e.CustomerId, "IX_ACCOUNTS_CUST_ID");

                entity.Property(e => e.AccountNo)
                    .HasColumnType("decimal(12, 0)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("account_no");

                entity.Property(e => e.AccountStatus)
                    .HasColumnName("account_status")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.AccountType).HasColumnName("account_type");

                entity.Property(e => e.Balance).HasColumnName("balance");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("created_on")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CustomerId).HasColumnName("customer_id");

                //entity.HasOne(d => d.AccountTypeNavigation)
                //    .WithMany(p => p.Accounts)
                //    .HasForeignKey(d => d.AccountType)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK__ACCOUNTS__accoun__31EC6D26");

                //entity.HasOne(d => d.Customer)
                //    .WithMany(p => p.Accounts)
                //    .HasForeignKey(d => d.CustomerId)
                //    .OnDelete(DeleteBehavior.Cascade)
                //    .HasConstraintName("FK__ACCOUNTS__custom__29572725");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("CUSTOMERS");

                entity.HasIndex(e => e.AadharNo, "UQ__CUSTOMER__AFE822FEAD475A56")
                    .IsUnique();

                entity.Property(e => e.CustomerId).HasColumnName("customer_id");

                entity.Property(e => e.AadharNo)
                    .HasMaxLength(12)
                    .HasColumnName("aadhar_no");

                entity.Property(e => e.Address)
                    .HasMaxLength(100)
                    .HasColumnName("address");

                entity.Property(e => e.DateOfBirth)
                    .HasColumnType("date")
                    .HasColumnName("date_of_birth");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .HasColumnName("email");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(30)
                    .HasColumnName("first_name");

                entity.Property(e => e.LastName)
                    .HasMaxLength(30)
                    .HasColumnName("last_name");

                entity.Property(e => e.MiddleName)
                    .HasMaxLength(30)
                    .HasColumnName("middle_name");

                entity.Property(e => e.Phone)
                    .HasMaxLength(10)
                    .HasColumnName("phone");
            });

            modelBuilder.Entity<Interest>(entity =>
            {
                entity.HasKey(e => e.AccountType)
                    .HasName("PK__INTEREST__089E3BE55BF80D4D");

                entity.ToTable("INTERESTS");

                entity.Property(e => e.AccountType)
                    .ValueGeneratedNever()
                    .HasColumnName("account_type");

                entity.Property(e => e.InterestRate).HasColumnName("interest_rate");
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.ToTable("TRANSACTIONS");

                entity.HasIndex(e => e.AccountNo, "IX_TRANSACTION_ACCOUNT_NO");

                entity.Property(e => e.TransactionId).HasColumnName("transaction_id");

                entity.Property(e => e.AccountNo)
                    .HasColumnType("decimal(12, 0)")
                    .HasColumnName("account_no");

                entity.Property(e => e.AvailableBalance).HasColumnName("AVAILABLE_BALANCE");

                entity.Property(e => e.TransactionAmount).HasColumnName("transaction_amount");

                entity.Property(e => e.TransactionTime)
                    .HasColumnType("datetime")
                    .HasColumnName("transaction_time")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.TransactionType).HasColumnName("transaction_type");

                //entity.HasOne(d => d.AccountNoNavigation)
                //    .WithMany(p => p.Transactions)
                //    .HasForeignKey(d => d.AccountNo)
                //    .OnDelete(DeleteBehavior.Cascade)
                //    .HasConstraintName("FK__TRANSACTI__accou__2E1BDC42");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

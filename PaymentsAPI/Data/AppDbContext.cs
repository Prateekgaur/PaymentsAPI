//namespace PaymentsAPI.Data
//{
//    public class AppDbContext
//    {
//    }

//}
using Microsoft.EntityFrameworkCore;
using PaymentsAPI.Models;
using System.Collections.Generic;

namespace PaymentsAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Payment> Payments { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure User entity
            modelBuilder.Entity<User>()
                .HasKey(u => u.Id); // Primary key

            modelBuilder.Entity<User>()
                .Property(u => u.Id)
                .ValueGeneratedOnAdd(); // Auto-increment

            // Configure Payment entity
            modelBuilder.Entity<Payment>()
                .HasKey(p => p.Id); // Primary key

            modelBuilder.Entity<Payment>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd(); // Auto-increment

            modelBuilder.Entity<Payment>()
            .Property(p => p.Status)
            .HasConversion<string>()
            .HasDefaultValue(PaymentStatus.Pending); // Store enums as strings
        }
    }    
}

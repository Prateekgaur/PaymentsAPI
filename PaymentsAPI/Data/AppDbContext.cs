﻿//namespace PaymentsAPI.Data
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
                .HasKey(u => u.Id); 

            modelBuilder.Entity<User>()
                .Property(u => u.Id)
                .ValueGeneratedOnAdd(); 

            modelBuilder.Entity<User>()
                .Property(u => u.Role)
                .HasDefaultValue("User"); 

            // Configure Payment entity
            modelBuilder.Entity<Payment>()
                .HasKey(p => p.Id); 

            modelBuilder.Entity<Payment>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd(); 

            modelBuilder.Entity<Payment>()
            .Property(p => p.Status)
            .HasConversion<string>()
            .HasDefaultValue(PaymentStatus.Pending);
        }
    }    
}
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
    }
}

using Microsoft.EntityFrameworkCore;
using PharmacyInventorySystem.Models;

namespace PharmacyInventorySystem.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }

        public DbSet<Supplier> Suppliers { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Sale> Sales { get; set; }
        public DbSet<Payment> Payments { get; set; }
    }
}   
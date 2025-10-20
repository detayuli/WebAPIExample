using Microsoft.EntityFrameworkCore;
using WebAPIExample.Data.Models;

namespace WebAPIExample.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        // ✅ Tambahkan seeding data
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Atur presisi decimal biar gak warning
            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasPrecision(18, 2);

            // Data kategori
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Makanan" },
                new Category { Id = 2, Name = "Minuman" },
                new Category { Id = 3, Name = "Elektronik" }
            );

            // Data produk contoh
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Nasi Goreng", Price = 20000, CategoryId = 1 },
                new Product { Id = 2, Name = "Es Teh Manis", Price = 8000, CategoryId = 2 },
                new Product { Id = 3, Name = "Kipas Angin", Price = 150000, CategoryId = 3 }
            );
        }
    }
}

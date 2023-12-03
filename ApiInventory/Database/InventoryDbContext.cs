using ApiInventory.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiInventory.Database;

public class InventoryDbContext : DbContext
{
    public DbSet<ProductModel> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=InventoryDb;Trusted_Connection=True;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProductModel>().HasAlternateKey(p => p.Code);
    }
}

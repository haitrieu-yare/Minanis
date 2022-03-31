using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    private DbSet<User> User => Set<User>();
    private DbSet<Product> Product => Set<Product>();
    private DbSet<ProductBatch> ProductBatch => Set<ProductBatch>();
    private DbSet<Order> Order => Set<Order>();
    private DbSet<OrderProduct> OrderProduct => Set<OrderProduct>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>().HasIndex(x => x.AccountName).IsUnique();
        modelBuilder.Entity<User>().HasIndex(x => x.Email).IsUnique();
        modelBuilder.Entity<Product>().HasIndex(x => x.Name).IsUnique();
    }
}
using Microsoft.EntityFrameworkCore;
using Store.Entities.Models;

public class StoreContext : DbContext
{
    public StoreContext(DbContextOptions<StoreContext> options) : base(options)
    {

    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>()
       .Property(p => p.Price)
       .HasColumnType("decimal(18, 2)");

        modelBuilder.Entity<Product>()
            .Property(p => p.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<Category>()
            .Property(c => c.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<User>()
            .Property(u => u.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<Order>()
            .Property(o => o.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<OrderItem>()
            .Property(oi => oi.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<Product>()
            .HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId);
            

        modelBuilder.Entity<Order>()
            .HasOne(o => o.User)
            .WithMany(u => u.Orders)
            .HasForeignKey(o => o.userId);

        modelBuilder.Entity<OrderItem>()
            .HasOne(oi => oi.Orders)
            .WithMany(o => o.OrderItems)
            .HasForeignKey(oi => oi.OrderId);

        modelBuilder.Entity<OrderItem>()
            .HasOne(oi => oi.Products)
            .WithMany(p => p.OrderItems)
            .HasForeignKey(oi => oi.ProductId);


        base.OnModelCreating(modelBuilder);
    }
}

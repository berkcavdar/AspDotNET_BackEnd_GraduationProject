using _00_LoginPage.Models;
using Microsoft.EntityFrameworkCore;

namespace _00_LoginPage.Context;

public class ShoppingDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }

    public ShoppingDbContext(DbContextOptions<ShoppingDbContext> options)
        : base(options)
    {
    }

   protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region User
        modelBuilder.Entity<User>(u => u.HasIndex(x => x.Email).IsUnique());
        modelBuilder.Entity<User>(u => u.Property(p => p.IsAdmin).HasDefaultValue(false));
        modelBuilder.Entity<User>(u => u.Property(p => p.FirstName).IsRequired());
        modelBuilder.Entity<User>(u => u.Property(p => p.LastName).IsRequired());
        modelBuilder.Entity<User>(u => u.Property(p => p.Email).IsRequired());
        modelBuilder.Entity<User>(u => u.Property(p => p.Password).IsRequired());

        modelBuilder.Entity<User>(u => u.HasMany(x => x.ShoppingLists)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId));
        #endregion

        #region Category
        modelBuilder.Entity<Category>(c => c.HasIndex(x => x.Name).IsUnique());
        modelBuilder.Entity<Category>(c => c.Property(p => p.Name).IsRequired());
        modelBuilder
            .Entity<Category>(c => c.HasMany(p => p.Products)
            .WithOne(x => x.Category)
            .HasForeignKey(x => x.CategoryId));
        #endregion

        #region Product
        #endregion
    }
}

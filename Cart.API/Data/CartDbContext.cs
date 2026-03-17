using Cart.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Cart.API.Data;

public class CartDbContext : DbContext
{
    public CartDbContext(DbContextOptions<CartDbContext> options) : base(options)
    {
    }

    public DbSet<ShoppingCart> Carts => Set<ShoppingCart>();
    public DbSet<CartItem> CartItems => Set<CartItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ShoppingCart>(entity =>
        {
            entity.ToTable("carts", "dbo");
            entity.HasKey(x => x.Id);

            entity.Property(x => x.Id).HasColumnName("id");
            entity.Property(x => x.UserId).HasColumnName("user_id");
            entity.Property(x => x.CreatedAt).HasColumnName("created_at");
            entity.Property(x => x.UpdatedAt).HasColumnName("updated_at");

            entity.HasMany(x => x.Items)
                  .WithOne()
                  .HasForeignKey(x => x.CartId);
        });

        modelBuilder.Entity<CartItem>(entity =>
        {
            entity.ToTable("cart_items", "dbo");
            entity.HasKey(x => x.Id);

            entity.Property(x => x.Id).HasColumnName("id");
            entity.Property(x => x.CartId).HasColumnName("cart_id");
            entity.Property(x => x.ProductId).HasColumnName("product_id");
            entity.Property(x => x.ProductName).HasColumnName("product_name");
            entity.Property(x => x.Quantity).HasColumnName("quantity");
            entity.Property(x => x.Price).HasColumnName("price");
            entity.Property(x => x.Currency).HasColumnName("currency");
        });
    }
}

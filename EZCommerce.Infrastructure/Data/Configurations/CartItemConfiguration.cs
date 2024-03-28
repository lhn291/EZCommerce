using EZCommerce.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace EZCommerce.Infrastructure.Data.Configurations
{
    public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {
            builder.HasKey(ci => ci.Id);

            builder.HasOne(ci => ci.ShoppingCart)
                   .WithMany(sc => sc.CartItems)
                   .HasForeignKey(ci => ci.ShoppingCartId);

            builder.HasOne(ci => ci.Product)
                   .WithMany()
                   .HasForeignKey(ci => ci.ProductId);

            builder.Property(ci => ci.Quantity)
                   .IsRequired();

        }
    }
}

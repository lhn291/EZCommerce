using EZCommerce.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EZCommerce.Infrastructure.Data.Configurations
{
    public class ShoppingCartConfiguration : IEntityTypeConfiguration<ShoppingCart>
    {
        public void Configure(EntityTypeBuilder<ShoppingCart> builder)
        {
            builder.HasKey(sc => sc.Id);

            builder.HasOne(sc => sc.User)
                   .WithMany(u => u.ShoppingCarts)
                   .HasForeignKey(sc => sc.UserId);

            builder.HasMany(sc => sc.CartItems)
                   .WithOne(ci => ci.ShoppingCart)
                   .HasForeignKey(ci => ci.ShoppingCartId);

        }
    }
}

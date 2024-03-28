using EZCommerce.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EZCommerce.Infrastructure.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                   .ValueGeneratedOnAdd();

            builder.HasOne(p => p.Category)
                   .WithMany(c => c.Products)
                   .HasForeignKey(p => p.CategoryId);

            builder.HasOne(p => p.Brand)
                   .WithMany(b => b.Products)
                   .HasForeignKey(p => p.BrandId);

            builder.HasMany(p => p.ProductReviews)
                   .WithOne(pr => pr.Product)
                   .HasForeignKey(pr => pr.ProductId);

            builder.HasMany(p => p.Discounts)
                   .WithOne(d => d.Product)
                   .HasForeignKey(d => d.ProductId);
        }
    }
}

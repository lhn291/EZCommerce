using EZCommerce.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EZCommerce.Infrastructure.Data.Configurations
{
    public class DiscountConfiguration : IEntityTypeConfiguration<Discount>
    {
        public void Configure(EntityTypeBuilder<Discount> builder)
        {
            builder.HasKey(d => d.Id);

            builder.HasOne(d => d.Product)
                   .WithMany(p => p.Discounts) 
                   .HasForeignKey(d => d.ProductId);

            builder.Property(d => d.Percentage)
                   .IsRequired();

            builder.Property(d => d.Percentage)
                   .HasDefaultValueSql("0");

        }
    }
}

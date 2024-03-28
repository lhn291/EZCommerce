using EZCommerce.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EZCommerce.Infrastructure.Data.Configurations
{
    public class ProductReviewConfiguration : IEntityTypeConfiguration<ProductReview>
    {
        public void Configure(EntityTypeBuilder<ProductReview> builder)
        {
            builder.HasKey(pr => pr.Id);

            builder.HasOne(pr => pr.User)
                   .WithMany(u => u.ProductReviews)
                   .HasForeignKey(pr => pr.UserId);

            builder.HasOne(pr => pr.Product)
                   .WithMany(p => p.ProductReviews)
                   .HasForeignKey(pr => pr.ProductId);
        }
    }
}

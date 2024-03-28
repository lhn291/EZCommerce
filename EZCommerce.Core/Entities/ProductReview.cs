namespace EZCommerce.Core.Entities
{
    public class ProductReview
    {
        public int Id { get; set; }
        public string UserId { get; set; } = null!;
        public ApplicationUser User { get; set; } = null!;
        public int ProductId { get; set; } = 0;
        public DateTime ReviewDate { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public Product? Product { get; set; }
        public string? ImageUrl { get; set; }
    }
}

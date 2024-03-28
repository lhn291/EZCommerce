namespace EZCommerce.Core.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public double Price { get; set; }
        public int StockQuantity { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int? CategoryId { get; set; }
        public Category Category { get; set; } = new Category();
        public double AverageRating { get; set; }
        public string? ImagePath { get; set; }
        public int? BrandId { get; set; }
        public Brand Brand { get; set; } = new Brand();
        public string? Location { get; set; }
        public string? ShopName { get; set; }
        public ICollection<ProductReview> ProductReviews { get; set; } = new List<ProductReview>();
        public string? Color { get; set; }
        public string? Size { get; set; }
        public ICollection<Discount> Discounts { get; set; } = new List<Discount>();
    }
}

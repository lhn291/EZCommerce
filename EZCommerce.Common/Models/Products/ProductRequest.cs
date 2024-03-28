namespace EZCommerce.Common.Models.Products
{
    public class ProductRequest
    {
        public string? Name { get; set; }
        public double Price { get; set; }
        public int StockQuantity { get; set; }
        public string? Description { get; set; }
        public int? CategoryId { get; set; }
        public int? BrandId { get; set; }
        public string? ShopName { get; set; }
    }
}
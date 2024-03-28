namespace EZCommerce.Common.Models.Products
{
    public class ProductResponse
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public double Price { get; set; }
        public int StockQuantity { get; set; }
        public string? Description { get; set; }
        public string? CategoryName { get; set; }
        public string? BrandName { get; set; }
        public string? ShopName { get; set; }
    }
}
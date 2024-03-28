namespace EZCommerce.Core.Entities
{
    public class Discount
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
        public double Percentage { get; set; }

    }
}

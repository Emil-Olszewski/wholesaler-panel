namespace Infrastructure.Persistence.Models
{
    public class ProductPrice
    {
        public long ProductId { get; set; }
        public Product Product { get; set; }
        public decimal Price { get; set; }
    }
}
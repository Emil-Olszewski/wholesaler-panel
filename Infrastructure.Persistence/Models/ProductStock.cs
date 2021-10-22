namespace Infrastructure.Persistence.Models
{
    public class ProductStock
    {
        public long ProductId { get; set; }
        public Product Product { get; set; }
        public int QuantityAvailable { get; set; }
    }
}
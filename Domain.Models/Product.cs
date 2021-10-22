using Domain.Models.Common;

namespace Domain.Models
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public double DiscountMultiplier { get; set; }
        public int QuantityAvailable { get; set; }
    }
}
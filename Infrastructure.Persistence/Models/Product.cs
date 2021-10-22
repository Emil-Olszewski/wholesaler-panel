using Infrastructure.Persistence.Models.Common;

namespace Infrastructure.Persistence.Models
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public ProductDiscount Discount { get; set; }
        public ProductPrice Price { get; set; }
        public ProductStock Stock { get; set; }
    }
}
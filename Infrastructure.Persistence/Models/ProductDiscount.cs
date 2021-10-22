using Infrastructure.Persistence.Models.Common;

namespace Infrastructure.Persistence.Models
{
    public class ProductDiscount : BaseDiscount
    {
        public long ProductId { get; set; }
        public Product Product { get; set; }
    }
}
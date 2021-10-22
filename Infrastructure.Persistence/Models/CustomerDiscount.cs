using Infrastructure.Persistence.Models.Common;

namespace Infrastructure.Persistence.Models
{
    public class CustomerDiscount : BaseDiscount
    {
        public long CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
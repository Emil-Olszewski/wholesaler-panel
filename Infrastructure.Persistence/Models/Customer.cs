using Infrastructure.Persistence.Models.Common;

namespace Infrastructure.Persistence.Models
{
    public class Customer : BaseEntity
    {
        public long Id { get; set; }
        public string FullName { get; set; }
        public CustomerDiscount Discount { get; set; }
    }
}
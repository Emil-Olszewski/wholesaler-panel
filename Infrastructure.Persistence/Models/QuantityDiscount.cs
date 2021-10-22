using Infrastructure.Persistence.Models.Common;

namespace Infrastructure.Persistence.Models
{
    public class QuantityDiscount : BaseDiscount
    {
        public int MinQuantity { get; set; }
    }
}
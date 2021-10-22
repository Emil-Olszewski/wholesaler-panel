namespace Domain.Models
{
    public class Customer
    {
        public long Id { get; set; }
        public string FullName { get; set; }
        public double DiscountMultiplier { get; set; }
    }
}
namespace Core.Application.DTOs
{
    public class CustomerResponse
    {
        public long Id { get; set; }
        public string FullName { get; set; }
        public double DiscountMultiplier { get; set; }
    }
}
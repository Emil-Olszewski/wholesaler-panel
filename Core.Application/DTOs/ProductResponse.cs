using System.Collections.Generic;

namespace Core.Application.DTOs
{
    public record ProductResponse
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int QuantityAvailable { get; set; }
        public decimal Price { get; set; }
        public List<QuantitySalePriceResponse> QuantitySalePrices { get; set; }
    }
}
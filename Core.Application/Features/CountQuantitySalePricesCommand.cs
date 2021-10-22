using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.Application.DTOs;
using Core.Application.Interfaces;
using Domain.Models;
using MediatR;

namespace Core.Application.Features
{
    public record CountQuantitySalePricesCommand : IRequest<List<QuantitySalePriceResponse>>
    {
        public decimal BasePrice { get; set; }
        public double CustomerDiscountMultiplier { get; set; }
        public double ProductDiscountMultiplier { get; set; }
    }
    
    public class CountSalePriceCommandHandler : IRequestHandler<CountQuantitySalePricesCommand, List<QuantitySalePriceResponse>>
    {
        private readonly IRepository repository;

        public CountSalePriceCommandHandler(IRepository repository)
        {
            this.repository = repository;
        }
        
        public Task<List<QuantitySalePriceResponse>> Handle(CountQuantitySalePricesCommand request, CancellationToken cancellationToken)
        {
            return CountSalePrice(request);
        }

        private async Task<List<QuantitySalePriceResponse>> CountSalePrice(CountQuantitySalePricesCommand request)
        {
            var quantityDiscounts = await repository.GetAllQuantityDiscountsAsync();
            decimal basePrice = CountPriceAfterCustomerAndProductDiscount(request);
            return CreateQuantityDiscount(quantityDiscounts, basePrice).ToList();
        }

        private decimal CountPriceAfterCustomerAndProductDiscount(CountQuantitySalePricesCommand request)
        {
            return request.BasePrice - request.BasePrice * (decimal)request.CustomerDiscountMultiplier -
                   request.BasePrice * (decimal)request.ProductDiscountMultiplier;
        }

        private static IEnumerable<QuantitySalePriceResponse> CreateQuantityDiscount(List<QuantityDiscount> quantityDiscounts, decimal basePrice)
        {
            foreach (var quantityDiscount in quantityDiscounts)
            {
                yield return new QuantitySalePriceResponse
                {
                    MinQuantity = quantityDiscount.MinQuantity,
                    SalePrice = Math.Round(CountPriceAfterQuantityDiscount(basePrice, quantityDiscount), 2, MidpointRounding.AwayFromZero)
                };
            }
        }

        private static decimal CountPriceAfterQuantityDiscount(decimal basePrice, QuantityDiscount quantityDiscount)
        {
            return basePrice * (decimal)(1 - quantityDiscount.Multiplier);
        }
    }
}
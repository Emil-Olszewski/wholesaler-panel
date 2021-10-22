using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Infrastructure.Persistence.Models;

namespace Infrastructure.Persistence.Storages
{
    public class FakeStorage
    {
        private const int NumberOfProducts = 25;
        private const int NumberOfCustomers = 3;
        private const int MaxDiscountPercentage = 25;
        private const int MinQuantity = 50;
        private const int MaxQuantity = 2000;

        private readonly Random random = new();
        
        public List<Product> Products { get; }
        public List<ProductPrice> ProductPrices { get; }
        public List<ProductDiscount> ProductDiscounts { get; }
        public List<ProductStock> ProductStocks { get; }
        public List<Customer> Customers { get; }
        public List<CustomerDiscount> CustomerDiscounts { get; }
        public List<QuantityDiscount> QuantityDiscounts { get; }

        public FakeStorage()
        {
            Products = new List<Product>();
            ProductPrices = new List<ProductPrice>();
            ProductDiscounts = new List<ProductDiscount>();
            ProductStocks = new List<ProductStock>();
            Customers = new List<Customer>();
            CustomerDiscounts = new List<CustomerDiscount>();
            QuantityDiscounts = new List<QuantityDiscount>();
            
            CreateProducts();
            CreateProductPrices();
            CreateProductDiscounts();
            CreateProductStocks();
            CreateCustomers();
            CreateCustomerDiscounts();
            CreateQuantityDiscounts();
        }
        
        private void CreateProducts()
        {
            for (int i = 0; i < NumberOfProducts; i++)
            {
                Products.Add(new Product
                {
                    Id = random.Next(100,1000000),
                    Name = Faker.InternetFaker.Domain(),
                });
            }
        }

        private void CreateProductPrices()
        {
            foreach (var product in Products)
            {
                ProductPrices.Add(new ProductPrice
                {
                    ProductId = product.Id,
                    Product = product,
                    Price = random.Next(100, 5000) + (decimal)(random.Next(0, 100) * 0.01)
                });

                product.Price = ProductPrices.Last();
            }
        }

        private void CreateProductDiscounts()
        {
            foreach (var product in Products)
            {
                ProductDiscounts.Add(new ProductDiscount()
                {
                    ProductId = product.Id,
                    Product = product,
                    Multiplier = GetRandomMultiplier()
                });

                product.Discount = ProductDiscounts.Last();
            }
        }

        private void CreateProductStocks()
        {
            foreach (var product in Products)
            {
                ProductStocks.Add(new ProductStock()
                {
                    ProductId = product.Id,
                    Product = product,
                    QuantityAvailable = random.Next(MinQuantity, MaxQuantity + 1)
                });

                product.Stock = ProductStocks.Last();
            }
        }

        private void CreateCustomers()
        {
            for (int i = 0; i < NumberOfCustomers; i++)
            {
                Customers.Add(new Customer()
                {
                    Id = random.Next(10,1000),
                    FullName = Faker.NameFaker.Name()
                });
            }
        }

        private void CreateCustomerDiscounts()
        {
            foreach (var customer in Customers)
            {
               CustomerDiscounts.Add(new CustomerDiscount
               {
                   Multiplier = GetRandomMultiplier(),
                   CustomerId = customer.Id,
                   Customer = customer
               });

               customer.Discount = CustomerDiscounts.Last();
            }
        }

        private void CreateQuantityDiscounts()
        {
            QuantityDiscounts.Add(new QuantityDiscount
            {
                Multiplier = 0.1,
                MinQuantity = 100
            });
            
            QuantityDiscounts.Add(new QuantityDiscount
            {
                Multiplier = 0.15,
                MinQuantity = 250
            });
            
            QuantityDiscounts.Add(new QuantityDiscount
            {
                Multiplier = 0.25,
                MinQuantity = 500
            });
            
            QuantityDiscounts.Add(new QuantityDiscount
            {
                Multiplier = 0.4,
                MinQuantity = 1000
            });
        }

        private double GetRandomMultiplier()
        {
            var randomMultiplier = random.Next(0, MaxDiscountPercentage);
            return randomMultiplier * 0.01;
        }
    }
}
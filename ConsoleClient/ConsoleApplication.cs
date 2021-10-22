using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Core.Application.DTOs;
using Core.Application.Features;
using Core.Application.Parameters;
using MediatR;

namespace ConsoleClient
{
    public class ConsoleApplication
    {
        private readonly IMediator mediator;
        
        public ConsoleApplication(IMediator mediator)
        {
            this.mediator = mediator;
        }
        
        public async Task Run()
        {
            var customer = await Login();
            while (true)
            {
                Console.Clear();
                
                var products = await GetProducts(customer.Id);
                if (products is null || !products.Any())
                {
                    Console.WriteLine("Nie znaleziono produktów odpowiadającym podanym parametrom.");
                    continue;
                }
                
                ShowProducts(products);  
                
                Console.WriteLine();
                Console.WriteLine("Kliknij dowolny przycisk aby kontynuować");
                Console.ReadKey();
            }
        }

        private static void ShowProducts(List<ProductResponse> products)
        {
            Console.WriteLine($"Znaleziono {products.Count} produktow.");
            foreach (var product in products)
            {
                Console.WriteLine();
                Console.WriteLine($"[ID:{product.Id}] {product.Name}");
                Console.WriteLine($"Dostępna ilość: {product.QuantityAvailable}");
                Console.WriteLine($"Cena bazowa: {product.Price} PLN");
                Console.WriteLine("Ceny dla ciebie:");
                foreach (var quantitySalePrice in product.QuantitySalePrices)
                {
                    Console.WriteLine($"\tod {quantitySalePrice.MinQuantity} sztuk - {quantitySalePrice.SalePrice} PLN / szt");
                }
            }
        }

        private async Task<List<ProductResponse>?> GetProducts(long customerId)
        {
            var parameters = ReadParameters();

            var products = await SendRequest(
            new GetAllProductsQuery
            {
                CustomerId = customerId,
                Parameters = parameters
            });

            return products;
        }

        private static ProductParameters ReadParameters()
        {
            var parameters = new ProductParameters();
            
            Console.WriteLine("Wpisz maksymalną cenę bazową produktu:");
            if(decimal.TryParse(Console.ReadLine(), NumberStyles.Currency, CultureInfo.InvariantCulture, out decimal maxPrice))
            {
                parameters.MaxPrice = maxPrice;
            }
            else
            {
                Console.WriteLine("Nieprawidlowy format.");
            }
            
            Console.WriteLine("Wpisz minimalną dostępną ilość produktu:");
            if (int.TryParse(Console.ReadLine(), NumberStyles.Number, CultureInfo.InvariantCulture, out int minQuantity))
            {
                parameters.MinQuantity = minQuantity;
            }
            else
            {
                Console.WriteLine("Nieprawidlowy format.");
            }
            
            return parameters;
        }

        private async Task<CustomerResponse> Login()
        {
            var customers = await SendRequest(new GetAllCustomersQuery());

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Użytkownicy:");
                foreach (var customer in customers)
                {
                    Console.WriteLine($"[ID:{customer.Id}] {customer.FullName} - rabat {customer.DiscountMultiplier * 100}%");
                }

                Console.WriteLine("Wpisz ID użytkownika żeby się zalogować.");
                string customerId = Console.ReadLine();
            
                if (long.TryParse(customerId, NumberStyles.Number, CultureInfo.InvariantCulture, out long parsedCustomerId))
                {
                    var result = await SendRequest(new GetCustomerByIdQuery() { CustomerId = parsedCustomerId});
                    if (result is not null && result != default)
                    {
                        return result;
                    }
                }
                else
                {
                    Console.WriteLine("Nieprawidlowy format.");
                }

                Console.WriteLine("Cos poszlo nie tak. Wcisnij dowolny przycisk aby ponowic probe.");
                Console.ReadKey();
            }
        }

        private async Task<T> SendRequest<T>(IRequest<T> request)
        {
            try
            {
                return await mediator.Send(request);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}. Wcisnij dowolny przycisk aby kontynuowac.");
                Console.ReadKey();
                return default;
            }
        }
    }
}
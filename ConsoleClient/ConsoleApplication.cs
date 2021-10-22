using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
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
            
            var customers = await mediator.Send(new GetAllCustomersQuery());
            Console.WriteLine("Użytkownicy:");
            foreach (var customer in customers)
            {
                Console.WriteLine($"[ID:{customer.Id}] {customer.FullName} - rabat {customer.DiscountMultiplier * 100}%");   
            }
            
            Console.WriteLine("Wpisz ID użytkownika żeby się zalogować.");

            string customerId;
            customerId = Console.ReadLine();

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();

            var parameters = new ProductParameters();
            Console.WriteLine("Wpisz maksymalną cenę bazową produktu:");
            parameters.MaxPrice = decimal.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
            Console.WriteLine("Wpisz minimalną dostępną ilość produktu:");
            parameters.MinQuantity = int.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
            Console.WriteLine("Produkty:");

            var products = await mediator.Send(new GetAllProductsQuery
            {
                CustomerId = long.Parse(customerId),
                Parameters = parameters
            });

            if (!products.Any())
            {
                Console.WriteLine("Nie znaleziono produktów odpowiadającym podanym parametrom.");
            }

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
    }
}
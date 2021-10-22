using System;
using System.Threading.Tasks;
using Core.Application;
using Infrastructure.Persistence;
using Infrastructure.Shared;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleClient
{
    public static class Program
    {
        private static ServiceProvider serviceProvider;
        
        public static async Task Main(string[] args)
        {
            RegisterServices();
            IServiceScope scope = serviceProvider.CreateScope();
            await RunApp(scope);
            DisposeServices();
        }

        private static async Task RunApp(IServiceScope scope)
        {
            var app = new ConsoleApplication(scope.ServiceProvider.GetService<IMediator>());
            await app.Run();
        }

        private static void RegisterServices()
        {
            var services = new ServiceCollection();
            
            services.AddApplicationLayer();
            services.AddPersistenceInfrastructure();
            services.AddSharedInfrastructure();
            
            serviceProvider = services.BuildServiceProvider(true);
        }
        
        private static void DisposeServices()
        {
            if (serviceProvider is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }
    }
}
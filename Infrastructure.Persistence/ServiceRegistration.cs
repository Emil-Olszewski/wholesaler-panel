using Core.Application.Interfaces;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Persistence.Storages;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<IRepository, FakeRepository>();
            services.AddSingleton<FakeStorage>();
        }
    }
}
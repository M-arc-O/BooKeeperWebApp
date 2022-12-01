using BooKeeperWebApp.Infrastructure.Entities;
using BooKeeperWebApp.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace BooKeeperWebApp.Infrastructure.Configuration;
public static class DependencyInjectionConfirugation
{
    public static void AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IGenericRepository<BankAccount>, GenericRepository<BankAccount>>();
        services.AddScoped<IGenericRepository<User>, GenericRepository<User>>();
    }
}

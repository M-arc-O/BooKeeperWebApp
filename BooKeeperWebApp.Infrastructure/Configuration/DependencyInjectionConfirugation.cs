using BooKeeperWebApp.Infrastructure.Entities;
using BooKeeperWebApp.Infrastructure.Entities.Bank;
using BooKeeperWebApp.Infrastructure.Entities.Investment;
using BooKeeperWebApp.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace BooKeeperWebApp.Infrastructure.Configuration;
public static class DependencyInjectionConfirugation
{
    public static void AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IGenericRepository<BankAccount>, GenericRepository<BankAccount>>();
        services.AddScoped<IGenericRepository<Book>, GenericRepository<Book>>();
        services.AddScoped<IGenericRepository<Event>, GenericRepository<Event>>();
        services.AddScoped<IGenericRepository<Mutation>, GenericRepository<Mutation>>();

        services.AddScoped<IGenericRepository<InvestmentAccount>, GenericRepository<InvestmentAccount>>();
        services.AddScoped<IGenericRepository<Investment>, GenericRepository<Investment>>();
        services.AddScoped<IGenericRepository<InvestmentValue>, GenericRepository<InvestmentValue>>();

        services.AddScoped<IGenericRepository<MonthlyValue>, GenericRepository<MonthlyValue>>();
        services.AddScoped<IGenericRepository<YearlyValue>, GenericRepository<YearlyValue>>();

        services.AddScoped<IGenericRepository<User>, GenericRepository<User>>();
    }
}

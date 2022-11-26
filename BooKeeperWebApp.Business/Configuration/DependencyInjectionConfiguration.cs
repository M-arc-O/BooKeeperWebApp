using BooKeeperWebApp.Business.Commands;
using BooKeeperWebApp.Business.CQRS;
using BooKeeperWebApp.Business.Models;
using BooKeeperWebApp.Business.Queries;
using BooKeeperWebApp.Business.Services;
using BooKeeperWebApp.Infrastructure.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace BooKeeperWebApp.Business.Configuration;
public static class DependencyInjectionConfiguration
{
    public static void AddBusinessServices(this IServiceCollection services)
    {
        services.AddScoped<IExecutor, Executor>();
        services.AddScoped<IUserService, UserService>();

        services.AddScoped<IHandler<GetAllAccountsQuery, IEnumerable<BankAccountModel>>, GetAllAccountsQueryHandler>();

        services.AddScoped<IHandler<AddBankAccountCommand, BankAccountModel>, AddBankAccountCommandHandler>();
    }
}

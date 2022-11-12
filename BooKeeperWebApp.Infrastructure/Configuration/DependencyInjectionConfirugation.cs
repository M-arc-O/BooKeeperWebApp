using BooKeeperWebApp.Infrastructure.Contexts;
using BooKeeperWebApp.Infrastructure.Entities;
using BooKeeperWebApp.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooKeeperWebApp.Infrastructure.Configuration;
public static class DependencyInjectionConfirugation
{
    public static void AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<BooKeeperWebAppDbContext>();
        services.AddScoped<IGenericRepository<BankAccount>, GenericRepository<BankAccount>>();
    }
}

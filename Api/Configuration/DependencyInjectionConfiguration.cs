using BooKeeperWebApp.Business.Configuration;
using BooKeeperWebApp.Infrastructure.Configuration;
using BooKeeperWebApp.Infrastructure.Contexts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Configuration;
public static class DependencyInjectionConfiguration
{
    public static void InitializeServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<BooKeeperWebAppDbContext>(options => 
            BooKeeperWebAppDbContext.ConfigureDbContextOptions(options, configuration.GetValue<string>("BooKeeperWebAppConnectionString")));

        services.AddBusinessServices();
        services.AddInfrastructureServices();
    }
}

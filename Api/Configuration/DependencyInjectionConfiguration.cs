using Api.MappingProfiles;
using Api.MappingProfiles.Overview;
using AutoMapper;
using BooKeeperWebApp.Business.Configuration;
using BooKeeperWebApp.Business.MappingProfiles;
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

        var mappingConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new BankAccountProfiles());
            mc.AddProfile(new BookProfiles());
            mc.AddProfile(new EventProfiles());
            mc.AddProfile(new MutationProfiles());
            mc.AddProfile(new UserProfiles());
            mc.AddProfile(new OverviewBookProfiles());

            mc.AddProfile(new BankAccountModelProfiles());
            mc.AddProfile(new BookModelProfiles());
            mc.AddProfile(new EventModelProfiles());
            mc.AddProfile(new MutationModelProfiles());
            mc.AddProfile(new UserModelProfiles());
        });

        IMapper mapper = mappingConfig.CreateMapper();
        services.AddSingleton(mapper);
    }
}

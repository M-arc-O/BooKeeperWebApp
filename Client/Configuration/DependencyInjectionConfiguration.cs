using AutoMapper;
using BooKeeperWebApp.Shared.Services.Csv;
using BooKeeperWebApp.Shared.Services.Csv.CsvModels;
using Client.Authentication;
using Client.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Radzen;

namespace Client.Configuration;
public static class DependencyInjectionConfiguration
{
    public static void InitializeServices(this IServiceCollection services)
    {
        services.AddScoped<AuthenticationStateProvider, ClientAuthenticationStateProvider>();
        services.AddScoped<ThemeService>();
        services.AddScoped<DialogService>();
        services.AddScoped<NotificationService>();
        services.AddScoped<BankAccountService>();
        services.AddScoped<BookService>();
        services.AddScoped<EventService>();
        services.AddScoped<MutationService>();
        services.AddTransient<ICsvService, CsvService>();

        var mappingConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new IngPaymentCsvModelMappingProfile());
        });

        IMapper mapper = mappingConfig.CreateMapper();
        services.AddSingleton(mapper);
    }
}

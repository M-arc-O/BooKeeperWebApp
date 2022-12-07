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
    }
}

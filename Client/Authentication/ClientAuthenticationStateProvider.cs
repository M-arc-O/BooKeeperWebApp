using BooKeeperWebApp.Shared.Extensions;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;
using System.Security.Claims;

namespace Client.Authentication;

public class ClientAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly HttpClient _httpClient;

    public string IdentityProvider { get; private set; } = string.Empty;

    public ClientAuthenticationStateProvider(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var claimsIdentity = new ClaimsIdentity();

        var response = await _httpClient.GetFromJsonAsync<ClientPrincipalPayLoad>("/.auth/me");

        if (response is not null)
        {
            if (response.ClientPrincipal is not null)
            {
                IdentityProvider = response.ClientPrincipal.IdentityProvider;
                claimsIdentity = response.ClientPrincipal.ToClaimsIdentity();
            }
        }

        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

        var authenticationState = new AuthenticationState(claimsPrincipal);

        return authenticationState;
    }
}

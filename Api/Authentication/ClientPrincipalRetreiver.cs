using BooKeeperWebApp.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Functions.Worker.Http;
using System.Text;
using System.Text.Json;

namespace Api.Authentication;
public class ClientPrincipalRetreiver
{
    public static ClientPrincipal? GetClientPrincipal(HttpRequestData req)
    {
        var clientPrincipal = new ClientPrincipal();

        if (req.Headers.TryGetValues("x-ms-client-principal", out var headers))
        {
            var data = headers.First();
            var decoded = Convert.FromBase64String(data);
            var json = Encoding.UTF8.GetString(decoded);
            clientPrincipal = JsonSerializer.Deserialize<ClientPrincipal>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        return clientPrincipal;
    }
}

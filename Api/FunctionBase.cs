using Api.Authentication;
using BooKeeperWebApp.Business.Models;
using BooKeeperWebApp.Business.Services;
using Microsoft.Azure.Functions.Worker.Http;

namespace Api;
public abstract class FunctionBase
{
    public readonly IUserService _userService;

    public FunctionBase(IUserService userService)
    {
        _userService = userService;
    }

    protected virtual async Task<UserModel> GetUserAsync(HttpRequestData req)
    {
        var user = ClientPrincipalRetreiver.GetClientPrincipal(req) ?? throw new Exception("Could not find user in request.");
        return await _userService.GetUserByProviderIdAsync(user.UserId);
    }
}

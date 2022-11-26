using Api.Authentication;
using BooKeeperWebApp.Business.Models;
using BooKeeperWebApp.Business.Services;
using Microsoft.Azure.Functions.Worker.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api;
public abstract class FunctionBase
{
    public readonly IUserService _userService;

    public FunctionBase(IUserService userService)
    {
        _userService = userService;
    }

    protected virtual async Task<UserModel> GetUser(HttpRequestData req)
    {
        var user = ClientPrincipalRetreiver.GetClientPrincipal(req) ?? throw new Exception("Could not find user in request.");
        return await _userService.GetUserByProviderId(user.UserId);
    }
}

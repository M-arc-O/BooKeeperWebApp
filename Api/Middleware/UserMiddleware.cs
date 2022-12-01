using Api.Authentication;
using AutoMapper;
using BooKeeperWebApp.Business.Models;
using BooKeeperWebApp.Business.Services;
using BooKeeperWebApp.Shared.Exceptions;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Middleware;

namespace Api.Middleware;
public class UserMiddleware : IFunctionsWorkerMiddleware
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public UserMiddleware(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    public async Task Invoke(FunctionContext context, FunctionExecutionDelegate next)
    {
        var req = await context.GetHttpRequestDataAsync() ?? throw new NotFoundException("Could not get request");

        if (req != null)
        {
            var user = _mapper.Map<UserModel>(ClientPrincipalRetreiver.GetClientPrincipal(req));
            await _userService.MakeSureUserExists(user);
        }

        await next(context);
    }
}

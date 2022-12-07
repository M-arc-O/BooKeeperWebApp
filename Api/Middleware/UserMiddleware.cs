using Api.Authentication;
using AutoMapper;
using BooKeeperWebApp.Business.Models;
using BooKeeperWebApp.Infrastructure.Contexts;
using BooKeeperWebApp.Infrastructure.Entities;
using BooKeeperWebApp.Shared.Exceptions;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Middleware;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Middleware;
public class UserMiddleware : IFunctionsWorkerMiddleware
{
    private readonly IMapper _mapper;

    public UserMiddleware(IMapper mapper)
    {
        _mapper = mapper;
    }

    public async Task Invoke(FunctionContext context, FunctionExecutionDelegate next)
    {
        var req = await context.GetHttpRequestDataAsync() ?? throw new NotFoundException("Could not get request");

        if (req != null)
        {
            var user = _mapper.Map<UserModel>(ClientPrincipalRetreiver.GetClientPrincipal(req));
            var userEntitie = _mapper.Map<User>(user);
            var dbContext = context.InstanceServices.GetService<BooKeeperWebAppDbContext>();

            if (dbContext != null && !dbContext.Users!.Any(u => u.ProviderId == userEntitie.ProviderId))
            {
                user.Id = Guid.NewGuid();
                await dbContext.Users!.AddAsync(userEntitie);
                await dbContext.SaveChangesAsync();
            }
        }

        await next(context);
    }
}

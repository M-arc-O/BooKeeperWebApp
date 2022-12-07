using Microsoft.Azure.Functions.Worker.Middleware;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.Functions.Worker.Http;
using System.Net;
using BooKeeperWebApp.Shared.Exceptions;
using BooKeeperWebApp.Shared.Dtos;

namespace Api.Middleware;
public class ExceptionHandlingMiddleware : IFunctionsWorkerMiddleware
{
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task Invoke(FunctionContext context, FunctionExecutionDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing invocation");

            var httpReqData = await context.GetHttpRequestDataAsync();

            if (httpReqData != null)
            {
                var newHttpResponse = httpReqData.CreateResponse(HttpStatusCode.InternalServerError);

                if (ex.InnerException != null)
                {
                    if (ex.InnerException is ValidationException)
                    {
                        newHttpResponse = httpReqData.CreateResponse(HttpStatusCode.BadRequest);
                    }

                    if (ex.InnerException is NotFoundException)
                    {
                        newHttpResponse = httpReqData.CreateResponse(HttpStatusCode.NotFound);
                    }

                    await newHttpResponse.WriteAsJsonAsync(
                        new ErrorDto { Message = ex.InnerException.Message }, 
                        newHttpResponse.StatusCode);
                }

                var invocationResult = context.GetInvocationResult();
                invocationResult.Value = newHttpResponse;
            }
        }
    }
}


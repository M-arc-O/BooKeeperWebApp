using System.Net;
using Api.Authentication;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace Api
{
    public class Me
    {
        private readonly ILogger _logger;

        public Me(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<Me>();
        }

        [Function("Me")]
        public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var myClientPrincipal = ClientPrincipalRetreiver.GetClientPrincipal(req);

            var response = req.CreateResponse(HttpStatusCode.InternalServerError);

            if (myClientPrincipal?.UserId is not null)
            {
                response = req.CreateResponse(HttpStatusCode.OK);
                response.WriteAsJsonAsync(myClientPrincipal);
            }

            return response;
        }
    }
}

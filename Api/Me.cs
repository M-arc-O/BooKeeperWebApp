using System.Net;
using Api.Authentication;
using Api.Models;
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
        public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var myResponse = new ClientPrincipalResponse();
            var myClientPrincipal = ClientPrincipalRetreiver.GetClientPrincipal(req);

            if (myClientPrincipal?.UserId is not null)
            {
                myResponse.ClientPrincipal = myClientPrincipal;
            }

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.WriteAsJsonAsync(myResponse);

            return response;
        }
    }
}

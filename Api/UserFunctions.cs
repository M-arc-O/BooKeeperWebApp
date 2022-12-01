using System.Net;
using Api.Authentication;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace Api
{
    public class UserFunctions
    {
        private readonly ILogger _logger;

        public UserFunctions(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<UserFunctions>();
        }

        [Function("GetUser")]
        public HttpResponseData GetUser([HttpTrigger(AuthorizationLevel.Function, "get", Route = "user/get")] HttpRequestData req)
        {
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

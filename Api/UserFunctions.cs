using System.Net;
using Api.Authentication;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

namespace Api
{
    public class UserFunctions
    {
        public UserFunctions()
        {
        }

        [Function("GetUser")]
        public async Task<HttpResponseData> GetUserAsync([HttpTrigger(AuthorizationLevel.Function, "get", Route = "user/get")] HttpRequestData req)
        {
            var myClientPrincipal = ClientPrincipalRetreiver.GetClientPrincipal(req);

            var response = req.CreateResponse(HttpStatusCode.InternalServerError);

            if (myClientPrincipal?.UserId is not null)
            {
                response = req.CreateResponse(HttpStatusCode.OK);
                await response.WriteAsJsonAsync(myClientPrincipal);
            }

            return response;
        }
    }
}

using System.Net;
using System.Text.Json;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace demo.Function
{
    public class Model
    {
        public string? Name { get; set; }
    }

    public class JustHello
    {
        private readonly ILogger _logger;

        public JustHello(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<JustHello>();
        }

        [Function("JustHello")]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonSerializer.Deserialize<Model>(requestBody);

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

            response.WriteString($"Welcome to Azure Functions! {data?.Name}");

            return response;
        }
    }
}

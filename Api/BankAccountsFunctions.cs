using System.IO;
using System.Net;
using System.Reflection;
using BooKeeperWebApp.Business;
using BooKeeperWebApp.Shared;
using BooKeeperWebApp.Shared.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Configuration;

namespace Api
{
    public class BankAccountFunctions
    {
        private readonly IBankAccountBusiness _bankAccountBusiness;

        public BankAccountFunctions(
            IBankAccountBusiness bankAccountBusiness
            )
        {
            _bankAccountBusiness = bankAccountBusiness;
        }

        [Function("GetConnectionString")]
        public async Task<HttpResponseData> GetConnectionString([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req)
        {
            var test = "Getting string";

            try
            {
                var location = Assembly.GetExecutingAssembly().Location;
                var directory = Path.GetDirectoryName(location);

                var configuration = new ConfigurationBuilder()
                    .SetBasePath(directory)
                    .AddJsonFile("local.settings.json", optional: true, reloadOnChange: false)
                    .AddEnvironmentVariables()
                    .Build();

                test = configuration.GetValue<string>("BooKeeperWebAppConnectionString");
            }
            catch (Exception ex)
            {
                test = $"Backend error: {ex.Message}";
            }

            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteAsJsonAsync(new TestModel(test));

            return response;
        }

        [Function("GetBankAccounts")]
        public async Task<HttpResponseData> GetBankAccounts([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req)
        {
            var bankAccounts = _bankAccountBusiness.GetBankAccounts();
            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteAsJsonAsync(bankAccounts);

            return response;
        }

        [Function("AddBankAccount")]
        public async Task<HttpResponseData> AddBankAccount([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req)
        {
            var bankAccount = await req.ReadFromJsonAsync<BankAccount>() ?? throw new Exception();
            await _bankAccountBusiness.AddBankAccount(bankAccount);
            var response = req.CreateResponse(HttpStatusCode.OK);

            return response;
        }
    }
}

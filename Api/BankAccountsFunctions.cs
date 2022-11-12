using System.Net;
using BooKeeperWebApp.Business;
using BooKeeperWebApp.Shared;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

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

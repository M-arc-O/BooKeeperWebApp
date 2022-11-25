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

        public BankAccountFunctions(IBankAccountBusiness bankAccountBusiness)
        {
            _bankAccountBusiness = bankAccountBusiness;
        }

        [Function("GetBankAccounts")]
        public async Task<HttpResponseData> GetBankAccounts([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req)
        {
            var response = req.CreateResponse(HttpStatusCode.OK);

            try
            {
                var bankAccounts = await _bankAccountBusiness.GetBankAccounts();
                await response.WriteAsJsonAsync(bankAccounts);
            }
            catch (Exception ex) 
            {
                var bankAccounts = new[] { new BankAccount(Guid.NewGuid(), ex.Message) };
                await response.WriteAsJsonAsync(bankAccounts);
            }

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

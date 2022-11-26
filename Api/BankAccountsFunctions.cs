using System.Net;
using Api.Authentication;
using AutoMapper;
using BooKeeperWebApp.Business.Commands;
using BooKeeperWebApp.Business.CQRS;
using BooKeeperWebApp.Business.Models;
using BooKeeperWebApp.Business.Queries;
using BooKeeperWebApp.Business.Services;
using BooKeeperWebApp.Shared.Dtos;
using BooKeeperWebApp.Shared.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

namespace Api
{
    public class BankAccountFunctions : FunctionBase
    {
        private readonly IExecutor _excecutor;
        private readonly IMapper _mapper;

        public BankAccountFunctions(IExecutor executor, IUserService userService, IMapper mapper)
            : base(userService)
        {
            _excecutor = executor;
            _mapper = mapper;
        }

        [Function("GetBankAccounts")]
        public async Task<HttpResponseData> GetBankAccounts([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req)
        {
            var response = req.CreateResponse(HttpStatusCode.OK);

            try
            {
                var user = await GetUser(req);
                var query = new GetAllAccountsQuery(user.Id);
                var bankAccounts = await _excecutor.Execute<GetAllAccountsQuery, IEnumerable<BankAccountModel>>(query);
                await response.WriteAsJsonAsync(bankAccounts);
            }
            catch (Exception ex) 
            {
                var bankAccounts = new[] { new BankAccountDto(Guid.NewGuid(), ex.Message) };
                await response.WriteAsJsonAsync(bankAccounts);
            }

            return response;
        }

        [Function("AddBankAccount")]
        public async Task<HttpResponseData> AddBankAccount([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req)
        {
            var bankAccount = await req.ReadFromJsonAsync<AddBankAccoutModel>() ?? throw new Exception();

            var user = await GetUser(req);

            var command = new AddBankAccountCommand(user.Id, bankAccount.Name);
            await _excecutor.Execute<AddBankAccountCommand, BankAccountModel>(command);

            var response = req.CreateResponse(HttpStatusCode.OK);

            return response;
        }
    }
}

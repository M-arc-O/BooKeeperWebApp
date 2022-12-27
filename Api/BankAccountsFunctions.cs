using System.Net;
using AutoMapper;
using BooKeeperWebApp.Business.Commands.BankAccount;
using BooKeeperWebApp.Business.CQRS;
using BooKeeperWebApp.Business.Models;
using BooKeeperWebApp.Business.Queries.BankAccount;
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
        public async Task<HttpResponseData> GetBankAccountsAsync(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "bankaccount/getall")] HttpRequestData req)
        {
            var response = req.CreateResponse(HttpStatusCode.OK);

            var user = await GetUserAsync(req);
            var query = new GetAllBankAccountsQuery(user.Id);
            var bankAccounts = await _excecutor.ExecuteAsync<GetAllBankAccountsQuery, IEnumerable<BankAccountModel>>(query);
            await response.WriteAsJsonAsync(bankAccounts.Select(x => _mapper.Map<BankAccountDto>(x)));

            return response;
        }

        [Function("GetBankAccountById")]
        public async Task<HttpResponseData> GetBankAccountById(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "bankaccount/getbyid/{id}")] HttpRequestData req, Guid id)
        {
            var response = req.CreateResponse(HttpStatusCode.OK);

            var user = await GetUserAsync(req);
            var query = new GetBankAccountByIdQuery(user.Id, id);
            var account = await _excecutor.ExecuteAsync<GetBankAccountByIdQuery, BankAccountModel>(query);
            await response.WriteAsJsonAsync(_mapper.Map<BankAccountDto>(account));

            return response;
        }

        [Function("CreateBankAccount")]
        public async Task<HttpResponseData> CreateBankAccountAsync([HttpTrigger(AuthorizationLevel.Function, "post", Route = "bankaccount/create")] HttpRequestData req)
        {
            var bankAccount = await req.ReadFromJsonAsync<AddBankAccountModel>() ?? throw new Exception();

            var user = await GetUserAsync(req);

            var command = new AddBankAccountCommand(user.Id, bankAccount.Name, bankAccount.Number, bankAccount.Type, bankAccount.StartAmount);
            var result = await _excecutor.ExecuteAsync<AddBankAccountCommand, BankAccountModel>(command);

            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteAsJsonAsync(_mapper.Map<BankAccountDto>(result));

            return response;
        }

        [Function("UpdateBankAccount")]
        public async Task<HttpResponseData> UpdateBankAccountAsync([HttpTrigger(AuthorizationLevel.Function, "put", Route = "bankaccount/{id}/update")] HttpRequestData req, Guid id)
        {
            var bankAccount = await req.ReadFromJsonAsync<AddBankAccountModel>() ?? throw new Exception();

            var user = await GetUserAsync(req);

            var command = new UpdateBankAccountCommand(user.Id, id, bankAccount.Name, bankAccount.Number, bankAccount.Type, bankAccount.StartAmount);
            var result = await _excecutor.ExecuteAsync<UpdateBankAccountCommand, BankAccountModel>(command);

            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteAsJsonAsync(_mapper.Map<BankAccountDto>(result));

            return response;
        }

        [Function("DeleteBankAccount")]
        public async Task<HttpResponseData> DeleteBankAccountAsync([HttpTrigger(AuthorizationLevel.Function, "delete", Route = "bankaccount/{id}/delete")] HttpRequestData req, Guid id)
        {
            var user = await GetUserAsync(req);

            var command = new DeleteBankAccountCommand(user.Id, id);
            var result = await _excecutor.ExecuteAsync<DeleteBankAccountCommand, Guid>(command);

            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteAsJsonAsync(result);

            return response;
        }
    }
}

using System.Net;
using AutoMapper;
using BooKeeperWebApp.Business.Commands.InvestmentAccount;
using BooKeeperWebApp.Business.CQRS;
using BooKeeperWebApp.Business.Models.Investment;
using BooKeeperWebApp.Business.Queries.InvestmentAccount;
using BooKeeperWebApp.Business.Services;
using BooKeeperWebApp.Shared.Dtos.Investment;
using BooKeeperWebApp.Shared.Models.Investment;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

namespace Api.InvestmentFunctions
{
    public class InvestmentAccountsFunctions : FunctionBase
    {
        private readonly IExecutor _excecutor;
        private readonly IMapper _mapper;

        public InvestmentAccountsFunctions(IExecutor executor, IUserService userService, IMapper mapper)
            : base(userService)
        {
            _excecutor = executor;
            _mapper = mapper;
        }

        [Function("GetInvestmentAccounts")]
        public async Task<HttpResponseData> GetInvestmentAccountsAsync(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "investmentaccount/getall")] HttpRequestData req)
        {
            var response = req.CreateResponse(HttpStatusCode.OK);

            var user = await GetUserAsync(req);
            var query = new GetAllInvestmentAccountsQuery(user.Id);
            var investmentAccounts = await _excecutor.ExecuteAsync<GetAllInvestmentAccountsQuery, IEnumerable<InvestmentAccountModel>>((GetAllInvestmentAccountsQuery)query);
            await response.WriteAsJsonAsync(investmentAccounts.Select(x => _mapper.Map<InvestmentAccountDto>(x)));

            return response;
        }

        [Function("GetInvestmentAccountById")]
        public async Task<HttpResponseData> GetInvestmentAccountById(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "investmentaccount/getbyid/{id}")] HttpRequestData req, Guid id)
        {
            var response = req.CreateResponse(HttpStatusCode.OK);

            var user = await GetUserAsync(req);
            var query = new GetInvestmentAccountByIdQuery(user.Id, id);
            var account = await _excecutor.ExecuteAsync<GetInvestmentAccountByIdQuery, InvestmentAccountModel>((GetInvestmentAccountByIdQuery)query);
            await response.WriteAsJsonAsync(_mapper.Map<InvestmentAccountDto>(account));

            return response;
        }

        [Function("CreateInvestmentAccount")]
        public async Task<HttpResponseData> CreateInvestmentAccountAsync([HttpTrigger(AuthorizationLevel.Function, "post", Route = "investmentaccount/create")] HttpRequestData req)
        {
            var investmentAccount = await req.ReadFromJsonAsync<AddInvestmentAccountModel>() ?? throw new Exception();

            var user = await GetUserAsync(req);

            var command = new AddInvestmentAccountCommand(user.Id, investmentAccount.Name, investmentAccount.Type);
            var result = await _excecutor.ExecuteAsync<AddInvestmentAccountCommand, InvestmentAccountModel>(command);

            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteAsJsonAsync(_mapper.Map<InvestmentAccountDto>(result));

            return response;
        }

        [Function("UpdateInvestmentAccount")]
        public async Task<HttpResponseData> UpdateInvestmentAccountAsync([HttpTrigger(AuthorizationLevel.Function, "put", Route = "investmentaccount/{id}/update")] HttpRequestData req, Guid id)
        {
            var investmentAccount = await req.ReadFromJsonAsync<AddInvestmentAccountModel>() ?? throw new Exception();

            var user = await GetUserAsync(req);

            var command = new UpdateInvestmentAccountCommand(user.Id, id, investmentAccount.Name, investmentAccount.Type);
            var result = await _excecutor.ExecuteAsync<UpdateInvestmentAccountCommand, InvestmentAccountModel>(command);

            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteAsJsonAsync(_mapper.Map<InvestmentAccountDto>(result));

            return response;
        }

        [Function("DeleteInvestmentAccount")]
        public async Task<HttpResponseData> DeleteInvestmentAccountAsync([HttpTrigger(AuthorizationLevel.Function, "delete", Route = "investmentaccount/{id}/delete")] HttpRequestData req, Guid id)
        {
            var user = await GetUserAsync(req);

            var command = new DeleteInvestmentAccountCommand(user.Id, id);
            var result = await _excecutor.ExecuteAsync<DeleteInvestmentAccountCommand, Guid>(command);

            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteAsJsonAsync(result);

            return response;
        }
    }
}

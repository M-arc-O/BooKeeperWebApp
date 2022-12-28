using System.Net;
using AutoMapper;
using BooKeeperWebApp.Business.Commands.Investment;
using BooKeeperWebApp.Business.CQRS;
using BooKeeperWebApp.Business.Models.Investment;
using BooKeeperWebApp.Business.Services;
using BooKeeperWebApp.Shared.Dtos.Investment;
using BooKeeperWebApp.Shared.Models.Investment;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

namespace Api.InvestmentFunctions
{
    public class InvestmentFunctions : FunctionBase
    {
        private readonly IExecutor _excecutor;
        private readonly IMapper _mapper;

        public InvestmentFunctions(IExecutor executor, IUserService userService, IMapper mapper)
            : base(userService)
        {
            _excecutor = executor;
            _mapper = mapper;
        }

        [Function("CreateInvestment")]
        public async Task<HttpResponseData> CreateInvestmentAsync([HttpTrigger(AuthorizationLevel.Function, "post", Route = "investment/create")] HttpRequestData req)
        {
            var investment = await req.ReadFromJsonAsync<AddInvestmentModel>() ?? throw new Exception();

            var user = await GetUserAsync(req);

            var command = new AddInvestmentCommand(user.Id, investment.InvestmentAccountId, investment.Name);
            var result = await _excecutor.ExecuteAsync<AddInvestmentCommand, InvestmentModel>(command);

            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteAsJsonAsync(_mapper.Map<InvestmentDto>(result));

            return response;
        }

        [Function("UpdateInvestment")]
        public async Task<HttpResponseData> UpdateInvestmentAsync([HttpTrigger(AuthorizationLevel.Function, "put", Route = "investment/{id}/update")] HttpRequestData req, Guid id)
        {
            var investment = await req.ReadFromJsonAsync<AddInvestmentModel>() ?? throw new Exception();

            var user = await GetUserAsync(req);

            var command = new UpdateInvestmentCommand(user.Id, id, investment.Name);
            var result = await _excecutor.ExecuteAsync<UpdateInvestmentCommand, InvestmentModel>(command);

            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteAsJsonAsync(_mapper.Map<InvestmentDto>(result));

            return response;
        }

        [Function("DeleteInvestment")]
        public async Task<HttpResponseData> DeleteInvestmentAsync([HttpTrigger(AuthorizationLevel.Function, "delete", Route = "investment/{id}/delete")] HttpRequestData req, Guid id)
        {
            var user = await GetUserAsync(req);

            var command = new DeleteInvestmentCommand(user.Id, id);
            var result = await _excecutor.ExecuteAsync<DeleteInvestmentCommand, Guid>(command);

            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteAsJsonAsync(result);

            return response;
        }
    }
}

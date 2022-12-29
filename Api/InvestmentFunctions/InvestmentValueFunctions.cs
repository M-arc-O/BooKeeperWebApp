using System.Net;
using AutoMapper;
using BooKeeperWebApp.Business.Commands.InvestmentValue;
using BooKeeperWebApp.Business.CQRS;
using BooKeeperWebApp.Business.Models.Investment;
using BooKeeperWebApp.Business.Services;
using BooKeeperWebApp.Shared.Dtos.Investment;
using BooKeeperWebApp.Shared.Models.Investment;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

namespace Api.InvestmentFunctions
{
    public class InvestmentValueFunctions : FunctionBase
    {
        private readonly IExecutor _excecutor;
        private readonly IMapper _mapper;

        public InvestmentValueFunctions(IExecutor executor, IUserService userService, IMapper mapper)
            : base(userService)
        {
            _excecutor = executor;
            _mapper = mapper;
        }

        [Function("CreateInvestmentValue")]
        public async Task<HttpResponseData> CreateInvestmentValueAsync([HttpTrigger(AuthorizationLevel.Function, "post", Route = "investmentvalue/create")] HttpRequestData req)
        {
            var investmentValue = await req.ReadFromJsonAsync<AddInvestmentValueModel>() ?? throw new Exception();

            var user = await GetUserAsync(req);

            var command = new AddInvestmentValueCommand(user.Id, investmentValue.InvestmentId, investmentValue.Date, investmentValue.Value);
            var result = await _excecutor.ExecuteAsync<AddInvestmentValueCommand, InvestmentValueModel>(command);

            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteAsJsonAsync(_mapper.Map<InvestmentValueDto>(result));

            return response;
        }

        [Function("DeleteInvestmentValue")]
        public async Task<HttpResponseData> DeleteInvestmentValueAsync([HttpTrigger(AuthorizationLevel.Function, "delete", Route = "investmentvalue/{id}/delete")] HttpRequestData req, Guid id)
        {
            var user = await GetUserAsync(req);

            var command = new DeleteInvestmentValueCommand(user.Id, id);
            var result = await _excecutor.ExecuteAsync<DeleteInvestmentValueCommand, Guid>(command);

            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteAsJsonAsync(result);

            return response;
        }
    }
}

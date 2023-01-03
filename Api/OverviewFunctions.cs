using System.Net;
using AutoMapper;
using BooKeeperWebApp.Business.CQRS;
using BooKeeperWebApp.Business.Models.Overview;
using BooKeeperWebApp.Business.Queries.Overview;
using BooKeeperWebApp.Business.Services;
using BooKeeperWebApp.Shared.Dtos.Overview;
using BooKeeperWebApp.Shared.Enums;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

namespace Api
{
    public class OverviewFunctions : FunctionBase 
    {
        private readonly IExecutor _excecutor;
        private readonly IMapper _mapper;

        public OverviewFunctions(IExecutor executor, IUserService userService, IMapper mapper)
            : base(userService)
        {
            _excecutor = executor;
            _mapper = mapper;
        }

        [Function("GetAccountsOverview")]
        public async Task<HttpResponseData> GetAccountsOverview([HttpTrigger(AuthorizationLevel.Function, "get", Route = "overview/getaccounts")] HttpRequestData req)
        {
            var response = req.CreateResponse(HttpStatusCode.OK);

            var user = await GetUserAsync(req);
            var query = new GetAccountsOverviewQuery(user.Id, DateTime.Now);
            var books = await _excecutor.ExecuteAsync<GetAccountsOverviewQuery, IEnumerable<OverviewAccountModel>>(query);
            await response.WriteAsJsonAsync(books.Select(x => _mapper.Map<OverviewAccountDto>(x)));

            return response;
        }

        [Function("GetAccountChart")]
        public async Task<HttpResponseData> GetAccountChart(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "overview/getaccountchart/{accountId}/{timespanType}/{numberOf}")] HttpRequestData req,
            Guid accountId,
            TimespanType timespanType,
            int numberOf)
        {
            var response = req.CreateResponse(HttpStatusCode.OK);

            var user = await GetUserAsync(req);
            var query = new GetAccountChartOverviewQuery(user.Id, accountId, timespanType, numberOf);
            var books = await _excecutor.ExecuteAsync<GetAccountChartOverviewQuery, IEnumerable<OverviewDateValueModel>>(query);
            await response.WriteAsJsonAsync(books.Select(x => _mapper.Map<OverviewDateValueDto>(x)));

            return response;
        }

        [Function("GetBooksOverview")]
        public async Task<HttpResponseData> GetBooksOverview([HttpTrigger(AuthorizationLevel.Function, "get", Route = "overview/getbooks/{year}")] HttpRequestData req, int year)
        {
            var response = req.CreateResponse(HttpStatusCode.OK);

            var user = await GetUserAsync(req);
            var query = new GetBooksOverviewQuery(user.Id, new DateTime(year, 1, 1));
            var books = await _excecutor.ExecuteAsync<GetBooksOverviewQuery, IEnumerable<OverviewBookModel>>(query);
            await response.WriteAsJsonAsync(books.Select(x => _mapper.Map<OverviewBookDto>(x)));

            return response;
        }
    }
}

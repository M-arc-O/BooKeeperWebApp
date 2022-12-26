using System.Net;
using AutoMapper;
using BooKeeperWebApp.Business.CQRS;
using BooKeeperWebApp.Business.Models.Overview;
using BooKeeperWebApp.Business.Queries.Overview;
using BooKeeperWebApp.Business.Services;
using BooKeeperWebApp.Shared.Dtos.Overview;
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

        [Function("GetBooksOverview")]
        public async Task<HttpResponseData> GetBooksOverview([HttpTrigger(AuthorizationLevel.Function, "get", Route = "overview/getbooks")] HttpRequestData req)
        {
            var response = req.CreateResponse(HttpStatusCode.OK);

            var user = await GetUserAsync(req);
            var query = new GetBooksOverviewQuery(user.Id, DateTime.Now);
            var books = await _excecutor.ExecuteAsync<GetBooksOverviewQuery, IEnumerable<OverviewBookModel>>(query);
            await response.WriteAsJsonAsync(books.Select(x => _mapper.Map<OverviewBookDto>(x)));

            return response;
        }
    }
}

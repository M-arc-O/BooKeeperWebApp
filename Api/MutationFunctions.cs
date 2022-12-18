using System.Net;
using AutoMapper;
using BooKeeperWebApp.Business.Commands.Mutation;
using BooKeeperWebApp.Business.CQRS;
using BooKeeperWebApp.Business.Models;
using BooKeeperWebApp.Business.Queries.Mutation;
using BooKeeperWebApp.Business.Services;
using BooKeeperWebApp.Shared.Dtos;
using BooKeeperWebApp.Shared.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

namespace Api
{
    public class MutationFunctions : FunctionBase
    {
        private readonly IExecutor _excecutor;
        private readonly IMapper _mapper;

        public MutationFunctions(IExecutor executor, IUserService userService, IMapper mapper)
            : base(userService)
        {
            _excecutor = executor;
            _mapper = mapper;
        }

        [Function("GetMutationsByAccountId")]
        public async Task<HttpResponseData> GetMutationsByAccountId(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "mutation/account/{accountId}")] HttpRequestData req, Guid accountId)
        {
            var response = req.CreateResponse(HttpStatusCode.OK);

            var user = await GetUserAsync(req);
            var query = new GetAllMutationsByAccountIdQuery(user.Id, accountId);
            var mutations = await _excecutor.ExecuteAsync<GetAllMutationsByAccountIdQuery, IEnumerable<MutationModel>>(query);
            await response.WriteAsJsonAsync(mutations.Select(x => _mapper.Map<MutationDto>(x)));

            return response;
        }

        [Function("GetMutationsByBookId")]
        public async Task<HttpResponseData> GetMutationsByBookId(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "mutation/book/{bookId}")] HttpRequestData req, Guid bookId)
        {
            var response = req.CreateResponse(HttpStatusCode.OK);

            var user = await GetUserAsync(req);
            var query = new GetAllMutationsByBookIdQuery(user.Id, bookId);
            var mutations = await _excecutor.ExecuteAsync<GetAllMutationsByBookIdQuery, IEnumerable<MutationModel>>(query);
            await response.WriteAsJsonAsync(mutations.Select(x => _mapper.Map<MutationDto>(x)));

            return response;
        }

        [Function("GetMutationsByEventId")]
        public async Task<HttpResponseData> GetMutationsByEventId(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "mutation/book/{eventId}")] HttpRequestData req, Guid eventId)
        {
            var response = req.CreateResponse(HttpStatusCode.OK);

            var user = await GetUserAsync(req);
            var query = new GetAllMutationsByEventIdQuery(user.Id, eventId);
            var mutations = await _excecutor.ExecuteAsync<GetAllMutationsByEventIdQuery, IEnumerable<MutationModel>>(query);
            await response.WriteAsJsonAsync(mutations.Select(x => _mapper.Map<MutationDto>(x)));

            return response;
        }

        [Function("CreateMutation")]
        public async Task<HttpResponseData> CreateMutationAsync([HttpTrigger(AuthorizationLevel.Function, "post", Route = "mutation/create")] HttpRequestData req)
        {
            var mutation = await req.ReadFromJsonAsync<AddMutationModel>() ?? throw new Exception();

            var user = await GetUserAsync(req);

            var command = new AddMutationCommand(
                user.Id, 
                mutation.Date,
                mutation.AccountNumber,
                mutation.OtherAccountNumber,
                mutation.Description,
                mutation.Comment,
                mutation.Tag,
                mutation.Amount,
                mutation.AmountAfterMutation,
                mutation.AccountId,
                mutation.BookId,
                mutation.EventId);
            var result = await _excecutor.ExecuteAsync<AddMutationCommand, MutationModel>(command);

            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteAsJsonAsync(_mapper.Map<MutationDto>(result));

            return response;
        }

        [Function("CreateMultipleMutation")]
        public async Task<HttpResponseData> CreateMultipleMutation([HttpTrigger(AuthorizationLevel.Function, "post", Route = "mutation/createmultiple")] HttpRequestData req)
        {
            var mutations = await req.ReadFromJsonAsync<AddMutationModel[]>() ?? throw new Exception();

            var user = await GetUserAsync(req);

            var mutationsToAdd = new List<AddMutationCommand>();

            foreach (var mutation in mutations)
            {
                mutationsToAdd.Add(new AddMutationCommand(user.Id,
                mutation.Date,
                mutation.AccountNumber,
                mutation.OtherAccountNumber,
                mutation.Description,
                mutation.Comment,
                mutation.Tag,
                mutation.Amount,
                mutation.AmountAfterMutation,
                mutation.AccountId,
                mutation.BookId,
                mutation.EventId));
            }

            var command = new AddMultipleMutationsCommand(mutationsToAdd.ToArray());
            await _excecutor.ExecuteAsync<AddMultipleMutationsCommand, MutationModel[]>(command);

            var response = req.CreateResponse(HttpStatusCode.OK);

            return response;
        }

        [Function("UpdateMutation")]
        public async Task<HttpResponseData> UpdateMutationAsync([HttpTrigger(AuthorizationLevel.Function, "put", Route = "mutation/{id}/update")] HttpRequestData req, Guid id)
        {
            var mutation = await req.ReadFromJsonAsync<AddMutationModel>() ?? throw new Exception();

            var user = await GetUserAsync(req);

            var command = new UpdateMutationCommand(user.Id, id, mutation.AccountId, mutation.BookId, mutation.EventId);
            var result = await _excecutor.ExecuteAsync<UpdateMutationCommand, MutationModel>(command);

            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteAsJsonAsync(_mapper.Map<MutationDto>(result));

            return response;
        }

        [Function("DeleteMutation")]
        public async Task<HttpResponseData> DeleteMutationAsync([HttpTrigger(AuthorizationLevel.Function, "delete", Route = "mutation/{id}/delete")] HttpRequestData req, Guid id)
        {
            var user = await GetUserAsync(req);

            var command = new DeleteMutationCommand(user.Id, id);
            var result = await _excecutor.ExecuteAsync<DeleteMutationCommand, Guid>(command);

            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteAsJsonAsync(result);

            return response;
        }
    }
}

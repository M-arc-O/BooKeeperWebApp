using System.Net;
using AutoMapper;
using BooKeeperWebApp.Business.Commands.Event;
using BooKeeperWebApp.Business.CQRS;
using BooKeeperWebApp.Business.Models;
using BooKeeperWebApp.Business.Queries.Event;
using BooKeeperWebApp.Business.Services;
using BooKeeperWebApp.Shared.Dtos;
using BooKeeperWebApp.Shared.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

namespace Api
{
    public class EventFunctions : FunctionBase
    {
        private readonly IExecutor _excecutor;
        private readonly IMapper _mapper;

        public EventFunctions(IExecutor executor, IUserService userService, IMapper mapper)
            : base(userService)
        {
            _excecutor = executor;
            _mapper = mapper;
        }

        [Function("GetEvents")]
        public async Task<HttpResponseData> GetBankAccountsAsync(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "event/getall")] HttpRequestData req)
        {
            var response = req.CreateResponse(HttpStatusCode.OK);

            var user = await GetUserAsync(req);
            var query = new GetAllEventsQuery(user.Id);
            var events = await _excecutor.ExecuteAsync<GetAllEventsQuery, IEnumerable<EventModel>>(query);
            await response.WriteAsJsonAsync(events.Select(x => _mapper.Map<EventDto>(x)));

            return response;
        }

        [Function("GetEventsById")]
        public async Task<HttpResponseData> GetEventsById(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "event/getbyid/{id}")] HttpRequestData req, Guid id)
        {
            var response = req.CreateResponse(HttpStatusCode.OK);

            var user = await GetUserAsync(req);
            var query = new GetEventByIdQuery(user.Id, id);
            var account = await _excecutor.ExecuteAsync<GetEventByIdQuery, EventModel>(query);
            await response.WriteAsJsonAsync(_mapper.Map<EventDto>(account));

            return response;
        }

        [Function("CreateEvent")]
        public async Task<HttpResponseData> CreateEventAsync([HttpTrigger(AuthorizationLevel.Function, "post", Route = "event/create")] HttpRequestData req)
        {
            var model = await req.ReadFromJsonAsync<AddEventModel>() ?? throw new Exception();

            var user = await GetUserAsync(req);

            var command = new AddEventCommand(user.Id, model.Name);
            var result = await _excecutor.ExecuteAsync<AddEventCommand, EventModel>(command);

            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteAsJsonAsync(_mapper.Map<EventDto>(result));

            return response;
        }

        [Function("UpdateEvent")]
        public async Task<HttpResponseData> UpdateEventAsync([HttpTrigger(AuthorizationLevel.Function, "put", Route = "event/{id}/update")] HttpRequestData req, Guid id)
        {
            var model = await req.ReadFromJsonAsync<AddEventModel>() ?? throw new Exception();

            var user = await GetUserAsync(req);

            var command = new UpdateEventCommand(user.Id, id, model.Name);
            var result = await _excecutor.ExecuteAsync<UpdateEventCommand, EventModel>(command);

            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteAsJsonAsync(_mapper.Map<EventDto>(result));

            return response;
        }

        [Function("DeleteEvent")]
        public async Task<HttpResponseData> DeleteEventAsync([HttpTrigger(AuthorizationLevel.Function, "delete", Route = "event/{id}/delete")] HttpRequestData req, Guid id)
        {
            var user = await GetUserAsync(req);

            var command = new DeleteEventCommand(user.Id, id);
            var result = await _excecutor.ExecuteAsync<DeleteEventCommand, Guid>(command);

            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteAsJsonAsync(result);

            return response;
        }
    }
}

using AutoMapper;
using BooKeeperWebApp.Business.CQRS;
using BooKeeperWebApp.Business.Models;
using BooKeeperWebApp.Infrastructure.Repositories;
using BooKeeperWebApp.Shared.Exceptions;

namespace BooKeeperWebApp.Business.Queries.Event;
public class GetEventByIdQueryHandler : IHandler<GetEventByIdQuery, EventModel>
{
    private readonly IGenericRepository<Infrastructure.Entities.Bank.Event> _eventRepository;
    private readonly IMapper _mapper;

    public GetEventByIdQueryHandler(IGenericRepository<Infrastructure.Entities.Bank.Event> eventRepository, IMapper mapper)
    {
        _eventRepository = eventRepository;
        _mapper = mapper;
    }

    public async Task<EventModel> ExecuteAsync(GetEventByIdQuery query)
    {
        var events = await _eventRepository.GetAsync(x => x.UserId == query.UserId, null, "Mutations");
        var eventEntitie = events.FirstOrDefault(x => x.Id == query.EventId)
            ?? throw new NotFoundException($"Event with id '{query.EventId}' could not be found.");

        return _mapper.Map<EventModel>(eventEntitie);
    }
}

using AutoMapper;
using BooKeeperWebApp.Business.CQRS;
using BooKeeperWebApp.Business.Models.Bank;
using BooKeeperWebApp.Infrastructure.Repositories;

namespace BooKeeperWebApp.Business.Queries.Event;
public class GetAllEventsQueryHandler : IHandler<GetAllEventsQuery, IEnumerable<EventModel>>
{
    private readonly IGenericRepository<Infrastructure.Entities.Bank.Event> _eventRepository;
    private readonly IMapper _mapper;

    public GetAllEventsQueryHandler(IGenericRepository<Infrastructure.Entities.Bank.Event> eventRepository, IMapper mapper)
    {
        _eventRepository = eventRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<EventModel>> ExecuteAsync(GetAllEventsQuery query)
    {
        var events = await _eventRepository.GetAsync(x => x.UserId == query.UserId);
        return events.Select(x => _mapper.Map<EventModel>(x));
    }
}

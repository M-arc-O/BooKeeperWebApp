using AutoMapper;
using BooKeeperWebApp.Business.CQRS;
using BooKeeperWebApp.Business.Models;
using BooKeeperWebApp.Infrastructure.Repositories;
using BooKeeperWebApp.Shared.Exceptions;

namespace BooKeeperWebApp.Business.Queries.Mutation;
public class GetAllMutationsByEventIdQueryHandler : IHandler<GetAllMutationsByEventIdQuery, IEnumerable<MutationModel>>
{
    protected readonly IGenericRepository<Infrastructure.Entities.Bank.Mutation> _mutationRepository;
    protected readonly IGenericRepository<Infrastructure.Entities.Bank.Event> _eventRepository;
    private readonly IMapper _mapper;

    public GetAllMutationsByEventIdQueryHandler(
        IGenericRepository<Infrastructure.Entities.Bank.Mutation> mutationRepository,
        IGenericRepository<Infrastructure.Entities.Bank.Event> eventRepository,
        IMapper mapper)
    {
        _mutationRepository = mutationRepository;
        _eventRepository = eventRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<MutationModel>> ExecuteAsync(GetAllMutationsByEventIdQuery query)
    {
        CheckIfUserHasAccessToEvent(query.UserId, query.EventId);

        var mutations = await _mutationRepository.GetAsync(x => x.Event != null && x.Event.Id == query.EventId);
        return mutations.Select(x => _mapper.Map<MutationModel>(x));
    }

    private async void CheckIfUserHasAccessToEvent(Guid userId, Guid eventId)
    {
        var eventEntity = await _eventRepository.GetByIdAsync(eventId) ?? throw new NotFoundException($"Event with id '{eventId}' not found.");

        if (eventEntity.UserId != userId)
        {
            throw new UnauthorizedAccessException($"User with id '{userId}' does not have access to event with id '{eventId}'.");
        }
    }
}

using BooKeeperWebApp.Infrastructure.Repositories;
using BooKeeperWebApp.Shared.Exceptions;

namespace BooKeeperWebApp.Business.Commands.Event;
public abstract class EventCommandBase
{
    protected readonly IGenericRepository<Infrastructure.Entities.Bank.Event> _eventRepository;

    protected EventCommandBase(IGenericRepository<Infrastructure.Entities.Bank.Event> eventRepository)
    {
        _eventRepository = eventRepository;
    }

    protected virtual async Task<Infrastructure.Entities.Bank.Event> GetEventAsync(Guid userId, Guid eventId)
    {
        var entitie = await _eventRepository.GetByIdAsync(eventId) ?? throw new NotFoundException($"Event with id '{eventId}' not found.");

        if (entitie.UserId != userId)
        {
            throw new UnauthorizedAccessException($"User with id '{userId}' does not have access to event with id '{eventId}'.");
        }

        return entitie;
    }

    protected virtual void ValidateName(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new ValidationException($"Event name cannot be empty");
        }
    }

    protected virtual async Task<bool> NameTakenAsync(string name)
    {
        var events = await _eventRepository.GetAsync(x => x.Name!.ToLower().Equals(name.ToLower()));
        return events.Any();
    }
}

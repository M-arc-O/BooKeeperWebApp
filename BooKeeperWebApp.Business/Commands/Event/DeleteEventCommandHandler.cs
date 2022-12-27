using BooKeeperWebApp.Business.CQRS;
using BooKeeperWebApp.Infrastructure.Repositories;

namespace BooKeeperWebApp.Business.Commands.Event;
public class DeleteEventCommandHandler : EventCommandBase, IHandler<DeleteEventCommand, Guid>
{
    private readonly IGenericRepository<Infrastructure.Entities.Bank.Event> _eventRepository;

    public DeleteEventCommandHandler(IGenericRepository<Infrastructure.Entities.Bank.Event> eventRepository)
        : base(eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public async Task<Guid> ExecuteAsync(DeleteEventCommand command)
    {
        var entitie = await GetEventAsync(command.UserId, command.EventId);
        _eventRepository.Delete(entitie);

        return command.EventId;
    }
}

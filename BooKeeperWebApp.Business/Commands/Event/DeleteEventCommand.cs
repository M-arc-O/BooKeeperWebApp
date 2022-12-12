using BooKeeperWebApp.Business.CQRS;

namespace BooKeeperWebApp.Business.Commands.Event;
public class DeleteEventCommand : ICommand
{
    public Guid UserId { get; }
    public Guid EventId { get; }

    public DeleteEventCommand(Guid userId, Guid eventId)
    {
        UserId = userId;
        EventId = eventId;
    }
}

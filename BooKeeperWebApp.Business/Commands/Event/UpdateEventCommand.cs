using BooKeeperWebApp.Business.CQRS;

namespace BooKeeperWebApp.Business.Commands.Event;
public class UpdateEventCommand : ICommand
{
    public Guid UserId { get; }
    public Guid EventId { get; }
    public string Name { get; }

    public UpdateEventCommand(Guid userId, Guid eventId, string name)
    {
        UserId = userId;
        EventId = eventId;
        Name = name;
    }
}

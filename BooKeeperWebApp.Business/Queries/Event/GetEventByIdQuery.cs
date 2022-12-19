using BooKeeperWebApp.Business.CQRS;

namespace BooKeeperWebApp.Business.Queries.Event;
public class GetEventByIdQuery : IQuery
{
    public Guid UserId { get; }

    public Guid EventId { get; }

    public GetEventByIdQuery(Guid userId, Guid eventId)
    {
        UserId = userId;
        EventId = eventId;
    }
}

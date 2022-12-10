using BooKeeperWebApp.Business.CQRS;

namespace BooKeeperWebApp.Business.Queries.Event;
public class GetAllEventsQuery : IQuery
{
    public Guid UserId { get; }

    public GetAllEventsQuery(Guid userId)
    {
        UserId = userId;
    }
}

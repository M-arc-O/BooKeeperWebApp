using BooKeeperWebApp.Business.CQRS;

namespace BooKeeperWebApp.Business.Queries.Mutation;
public class GetAllMutationsByEventIdQuery : IQuery
{
    public Guid UserId { get; }
    public Guid EventId { get; }

    public GetAllMutationsByEventIdQuery(Guid userId, Guid bookId)
    {
        UserId = userId;
        EventId = bookId;
    }
}

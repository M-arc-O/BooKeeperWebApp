using BooKeeperWebApp.Business.CQRS;

namespace BooKeeperWebApp.Business.Queries.Mutation;
public class GetAllMutationsByBookIdQuery : IQuery
{
    public Guid UserId { get; }
    public Guid BookId { get; }

    public GetAllMutationsByBookIdQuery(Guid userId, Guid bookId)
    {
        UserId = userId;
        BookId = bookId;
    }
}

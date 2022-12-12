using BooKeeperWebApp.Business.CQRS;

namespace BooKeeperWebApp.Business.Queries.Mutation;
public class GetAllMutationsByAccountIdQuery : IQuery
{
    public Guid UserId { get; }
    public Guid AccountId { get; }

    public GetAllMutationsByAccountIdQuery(Guid userId, Guid accountId)
    {
        UserId = userId;
        AccountId = accountId;
    }
}

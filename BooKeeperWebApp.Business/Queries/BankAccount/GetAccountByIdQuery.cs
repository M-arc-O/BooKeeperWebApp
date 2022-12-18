using BooKeeperWebApp.Business.CQRS;

namespace BooKeeperWebApp.Business.Queries.BankAccount;
public class GetAccountByIdQuery : IQuery
{
    public Guid UserId { get; }

    public Guid AccountId { get; }

    public GetAccountByIdQuery(Guid userId, Guid accountId)
    {
        UserId = userId;
        AccountId = accountId;
    }
}

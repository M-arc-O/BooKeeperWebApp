using BooKeeperWebApp.Business.CQRS;

namespace BooKeeperWebApp.Business.Queries.BankAccount;
public class GetBankAccountByIdQuery : IQuery
{
    public Guid UserId { get; }

    public Guid AccountId { get; }

    public GetBankAccountByIdQuery(Guid userId, Guid accountId)
    {
        UserId = userId;
        AccountId = accountId;
    }
}

using BooKeeperWebApp.Business.CQRS;

namespace BooKeeperWebApp.Business.Queries.BankAccount;
public class GetAllBankAccountsQuery : IQuery
{
    public Guid UserId { get; }

    public GetAllBankAccountsQuery(Guid userId)
    {
        UserId = userId;
    }
}

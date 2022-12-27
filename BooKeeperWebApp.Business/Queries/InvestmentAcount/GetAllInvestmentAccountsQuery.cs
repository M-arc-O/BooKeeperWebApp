using BooKeeperWebApp.Business.CQRS;

namespace BooKeeperWebApp.Business.Queries.InvestmentAccount;
public class GetAllInvestmentAccountsQuery : IQuery
{
    public Guid UserId { get; }

    public GetAllInvestmentAccountsQuery(Guid userId)
    {
        UserId = userId;
    }
}

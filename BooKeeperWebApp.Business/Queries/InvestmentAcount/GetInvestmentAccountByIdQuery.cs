using BooKeeperWebApp.Business.CQRS;

namespace BooKeeperWebApp.Business.Queries.InvestmentAccount;
public class GetInvestmentAccountByIdQuery : IQuery
{
    public Guid UserId { get; }

    public Guid AccountId { get; }

    public GetInvestmentAccountByIdQuery(Guid userId, Guid accountId)
    {
        UserId = userId;
        AccountId = accountId;
    }
}

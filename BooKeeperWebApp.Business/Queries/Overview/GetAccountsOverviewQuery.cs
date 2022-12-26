using BooKeeperWebApp.Business.CQRS;

namespace BooKeeperWebApp.Business.Queries.Overview;
public class GetAccountsOverviewQuery : IQuery
{
    public Guid UserId { get; }
    public DateTime Date { get; }

    public GetAccountsOverviewQuery(Guid userId, DateTime date)
    {
        UserId = userId;
        Date = date;
    }
}

using BooKeeperWebApp.Business.CQRS;

namespace BooKeeperWebApp.Business.Queries.Overview;
public class GetAccountChartOverviewQuery : IQuery
{
    public Guid UserId { get; }

    public GetAccountChartOverviewQuery(Guid userId)
    {
        UserId = userId;
    }
}

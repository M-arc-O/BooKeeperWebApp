using BooKeeperWebApp.Business.CQRS;
using BooKeeperWebApp.Shared.Enums;

namespace BooKeeperWebApp.Business.Queries.Overview;
public class GetAccountChartOverviewQuery : IQuery
{
    public Guid UserId { get; }
    public Guid AccountId { get; }
    public TimespanType TimespanType { get; }
    public int NumberOf { get; }

    public GetAccountChartOverviewQuery(Guid userId, Guid accountId, TimespanType timespanType, int numberOf)
    {
        UserId = userId;
        AccountId = accountId;
        TimespanType = timespanType;
        NumberOf = numberOf;
    }
}

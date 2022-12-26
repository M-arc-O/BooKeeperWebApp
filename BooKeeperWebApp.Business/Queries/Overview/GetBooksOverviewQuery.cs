using BooKeeperWebApp.Business.CQRS;

namespace BooKeeperWebApp.Business.Queries.Overview;
public class GetBooksOverviewQuery : IQuery
{
    public Guid UserId { get; }
    public DateTime Date { get; }

    public GetBooksOverviewQuery(Guid userId, DateTime date)
    {
        UserId = userId;
        Date = date;
    }
}

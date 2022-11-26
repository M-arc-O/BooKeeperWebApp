using BooKeeperWebApp.Business.CQRS;

namespace BooKeeperWebApp.Business.Queries;
public class GetAllAccountsQuery : IQuery
{
    public Guid UserId { get; }

	public GetAllAccountsQuery(Guid userId)
	{
		UserId = userId;
	}
}

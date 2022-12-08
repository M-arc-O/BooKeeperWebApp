using BooKeeperWebApp.Business.CQRS;

namespace BooKeeperWebApp.Business.Queries.Book;
public class GetAllBooksQuery : IQuery
{
    public Guid UserId { get; }

    public GetAllBooksQuery(Guid userId)
    {
        UserId = userId;
    }
}

using BooKeeperWebApp.Business.CQRS;

namespace BooKeeperWebApp.Business.Queries.Book;
public class GetBookByIdQuery : IQuery
{
    public Guid UserId { get; }

    public Guid BookId { get; }

    public GetBookByIdQuery(Guid userId, Guid bookId)
    {
        UserId = userId;
        BookId = bookId;
    }
}

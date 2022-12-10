using BooKeeperWebApp.Business.CQRS;

namespace BooKeeperWebApp.Business.Commands.Book;
public class DeleteBookCommand : ICommand
{
    public Guid UserId { get; }
    public Guid BookId { get; }

    public DeleteBookCommand(Guid userId, Guid bookId)
    {
        UserId = userId;
        BookId = bookId;
    }
}

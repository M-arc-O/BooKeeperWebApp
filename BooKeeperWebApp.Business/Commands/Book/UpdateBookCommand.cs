using BooKeeperWebApp.Business.CQRS;

namespace BooKeeperWebApp.Business.Commands.Book;
public class UpdateBookCommand : ICommand
{
    public Guid UserId { get; }
    public Guid BookId { get; }
    public string Name { get; }

    public UpdateBookCommand(Guid userId, Guid bookId, string name)
    {
        UserId = userId;
        BookId = bookId;
        Name = name;
    }
}

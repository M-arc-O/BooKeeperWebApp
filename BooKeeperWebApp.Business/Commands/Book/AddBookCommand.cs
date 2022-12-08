using BooKeeperWebApp.Business.CQRS;

namespace BooKeeperWebApp.Business.Commands.Book;
public class AddBookCommand : ICommand
{
    public Guid UserId { get; }
    public string Name { get; }

    public AddBookCommand(Guid userId, string name)
    {
        UserId = userId;
        Name = name;
    }
}

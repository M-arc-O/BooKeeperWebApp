using BooKeeperWebApp.Business.CQRS;

namespace BooKeeperWebApp.Business.Commands.Book;
public class DeleteBookCommand : ICommand
{
    public Guid UserId { get; }
    public Guid AccountId { get; }

    public DeleteBookCommand(Guid userId, Guid accountId)
    {
        UserId = userId;
        AccountId = accountId;
    }
}

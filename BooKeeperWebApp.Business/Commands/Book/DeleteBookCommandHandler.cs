using BooKeeperWebApp.Business.CQRS;
using BooKeeperWebApp.Infrastructure.Repositories;

namespace BooKeeperWebApp.Business.Commands.Book;
public class DeleteBookCommandHandler : BookCommandBase, IHandler<DeleteBookCommand, Guid>
{
    private readonly IGenericRepository<Infrastructure.Entities.Bank.Book> _bookRepository;

    public DeleteBookCommandHandler(IGenericRepository<Infrastructure.Entities.Bank.Book> bookRepository)
        : base(bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<Guid> ExecuteAsync(DeleteBookCommand command)
    {
        var book = await GetBookAsync(command.UserId, command.BookId);
        _bookRepository.Delete(book);

        return command.BookId;
    }
}

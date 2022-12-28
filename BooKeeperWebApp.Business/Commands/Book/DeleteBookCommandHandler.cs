using BooKeeperWebApp.Business.CQRS;
using BooKeeperWebApp.Infrastructure.Repositories;
using BooKeeperWebApp.Shared.Exceptions;

namespace BooKeeperWebApp.Business.Commands.Book;
public class DeleteBookCommandHandler : BookCommandBase, IHandler<DeleteBookCommand, Guid>
{
    public DeleteBookCommandHandler(IGenericRepository<Infrastructure.Entities.Bank.Book> bookRepository)
        : base(bookRepository)
    {
    }

    public async Task<Guid> ExecuteAsync(DeleteBookCommand command)
    {
        var book = await GetBookAsync(command.UserId, command.BookId);

        if (book.Mutations == null || book.Mutations.Any())
        {
            throw new ValidationException("This book still has mutations, please assign them to another book before deleting this one.");
        }

        _bookRepository.Delete(book);

        return command.BookId;
    }
}

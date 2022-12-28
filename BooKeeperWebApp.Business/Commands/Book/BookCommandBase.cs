using BooKeeperWebApp.Infrastructure.Repositories;
using BooKeeperWebApp.Shared.Exceptions;

namespace BooKeeperWebApp.Business.Commands.Book;
public abstract class BookCommandBase
{
    protected readonly IGenericRepository<Infrastructure.Entities.Bank.Book> _bookRepository;

    protected BookCommandBase(IGenericRepository<Infrastructure.Entities.Bank.Book> bookRepository)
    {
        _bookRepository = bookRepository;
    }

    protected virtual async Task<Infrastructure.Entities.Bank.Book> GetBookAsync(Guid userId, Guid bookId)
    {
        var book = await _bookRepository.GetByIdAsync(bookId) ?? throw new NotFoundException($"Book with id '{bookId}' not found.");

        if (book.UserId != userId)
        {
            throw new UnauthorizedAccessException($"User with id '{userId}' does not have access to book with id '{bookId}'.");
        }

        return book;
    }

    protected virtual void ValidateName(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new ValidationException($"Book name cannot be empty");
        }
    }

    protected virtual async Task<bool> NameTakenAsync(string name)
    {
        var books = await _bookRepository.GetAsync(x => x.Name!.ToLower().Equals(name.ToLower()));
        return books.Any();
    }
}

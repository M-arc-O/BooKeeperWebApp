using BooKeeperWebApp.Business.CQRS;
using BooKeeperWebApp.Infrastructure.Repositories;
using BooKeeperWebApp.Shared.Exceptions;

namespace BooKeeperWebApp.Business.Commands.Book;
public class DeleteBookCommandHandler : BookCommandBase, IHandler<DeleteBookCommand, Guid>
{
    private readonly IGenericRepository<Infrastructure.Entities.MonthlyValue> _monthlyValueRepository;
    private readonly IGenericRepository<Infrastructure.Entities.YearlyValue> _yearlyValueRepository;

    public DeleteBookCommandHandler(
        IGenericRepository<Infrastructure.Entities.Bank.Book> bookRepository,
        IGenericRepository<Infrastructure.Entities.MonthlyValue> monthlyValueRepository,
        IGenericRepository<Infrastructure.Entities.YearlyValue> yearlyValueRepository)
        : base(bookRepository)
    {
        _monthlyValueRepository = monthlyValueRepository;
        _yearlyValueRepository = yearlyValueRepository;
    }

    public async Task<Guid> ExecuteAsync(DeleteBookCommand command)
    {
        var books = await _bookRepository.GetAsync(x => x.Id == command.BookId && x.UserId == command.UserId, null, "MonthlyValues,YearlyValues");
        var book = books.FirstOrDefault() ?? throw new NotFoundException($"Book with id '{command.BookId}' not found.");

        if (book.Mutations == null || book.Mutations.Any())
        {
            throw new ValidationException("This book still has mutations, please assign them to another book before deleting this one.");
        }

        foreach (var monlthlyValue in book.MonthlyValues!)
        {
            _monthlyValueRepository.Delete(monlthlyValue);
        }

        foreach (var yearlyValue in book.YearlyValues!)
        {
            _yearlyValueRepository.Delete(yearlyValue);
        }

        _bookRepository.Delete(book);

        return command.BookId;
    }
}

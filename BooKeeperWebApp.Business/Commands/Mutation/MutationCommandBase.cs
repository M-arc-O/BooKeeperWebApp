using BooKeeperWebApp.Infrastructure.Repositories;
using BooKeeperWebApp.Shared.Exceptions;

namespace BooKeeperWebApp.Business.Commands.Mutation;
public abstract class MutationCommandBase
{
    private readonly IGenericRepository<Infrastructure.Entities.BankAccount> _accountRepository;
    private readonly IGenericRepository<Infrastructure.Entities.Book> _bookRepository;
    private readonly IGenericRepository<Infrastructure.Entities.Event> _eventRepository;
    protected readonly IGenericRepository<Infrastructure.Entities.Mutation> _mutationRepository;

    protected MutationCommandBase(
        IGenericRepository<Infrastructure.Entities.BankAccount> accountRepository,
        IGenericRepository<Infrastructure.Entities.Book> bookRepository,
        IGenericRepository<Infrastructure.Entities.Event> eventRepository,
        IGenericRepository<Infrastructure.Entities.Mutation> mutationRepository)
    {
        _accountRepository = accountRepository;
        _bookRepository = bookRepository;
        _eventRepository = eventRepository;
        _mutationRepository = mutationRepository;
    }

    protected virtual async Task<Infrastructure.Entities.BankAccount> GetAccountAsync(Guid userId, Guid accountId)
    {
        var bankAccount = await _accountRepository.GetByIdAsync(accountId) ?? throw new NotFoundException($"Bank account with id '{accountId}' not found.");

        if (bankAccount.UserId != userId)
        {
            throw new UnauthorizedAccessException($"User with id '{userId}' does not have access to bank account with id '{accountId}'.");
        }

        return bankAccount;
    }

    protected virtual async Task<Infrastructure.Entities.Book> GetBookAsync(Guid userId, Guid bookId)
    {
        var book = await _bookRepository.GetByIdAsync(bookId) ?? throw new NotFoundException($"Book with id '{bookId}' not found.");

        if (book.UserId != userId)
        {
            throw new UnauthorizedAccessException($"User with id '{userId}' does not have access to book with id '{bookId}'.");
        }

        return book;
    }

    protected virtual async Task<Infrastructure.Entities.Event> GetEventAsync(Guid userId, Guid eventId)
    {
        var entitie = await _eventRepository.GetByIdAsync(eventId) ?? throw new NotFoundException($"Event with id '{eventId}' not found.");

        if (entitie.UserId != userId)
        {
            throw new UnauthorizedAccessException($"User with id '{userId}' does not have access to event with id '{eventId}'.");
        }

        return entitie;
    }

    protected virtual async Task<Infrastructure.Entities.Mutation> GetMutationAsync(Guid userId, Guid mutationId)
    {
        var mutation = await _mutationRepository.GetByIdAsync(mutationId)
            ?? throw new NotFoundException($"Mutation with id '{mutationId}' not found.");

        if (mutation.Account.UserId != userId && 
            mutation.Book.UserId != userId && 
            mutation.Event?.UserId != userId)
        {
            throw new UnauthorizedAccessException($"User with id '{userId}' does not have access to mutation with id '{mutationId}'");
        }

        return mutation;
    }

    protected virtual async Task<bool> MutionExistsAsync(Infrastructure.Entities.Mutation mutation)
    {
        var events = await _mutationRepository.GetAsync(x => x.Equals(mutation));
        return events.Any();
    }
}

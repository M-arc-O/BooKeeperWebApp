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

    protected virtual async Task<Infrastructure.Entities.BankAccount> GetAccountAsync(Guid userId, string accountNumber)
    {
        var accounts = await _accountRepository.GetAsync(x => x.Number!.ToLower().Equals(accountNumber.ToLower()));
        var account = accounts.FirstOrDefault() ?? throw new NotFoundException($"Bank account with number '{accountNumber}' not found.");

        if (account.UserId != userId)
        {
            throw new UnauthorizedAccessException($"User with id '{userId}' does not have access to bank account with id '{account.Id}'.");
        }

        return account;
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
        var events = await _mutationRepository.GetAsync(x => x.Date == mutation!.Date &&
            x.AccountNumber.Equals(mutation!.AccountNumber) &&
            x.OtherAccountNumber.Equals(mutation!.OtherAccountNumber) &&
            x.Description.Equals(mutation!.Description) &&
            x.Comment != null && x.Comment.Equals(mutation.Comment) &&
            x.Tag != null && x.Tag.Equals(mutation.Tag) &&
            x.Amount == mutation!.Amount &&
            x.AmountAfterMutation == mutation!.AmountAfterMutation);
        return events.Any();
    }

    protected async Task<Infrastructure.Entities.Mutation> CreateMutation(AddMutationCommand command)
    {
        var entitie = new Infrastructure.Entities.Mutation
        {
            Id = Guid.NewGuid(),
            Date = command.Date,
            AccountNumber = command.AccountNumber,
            OtherAccountNumber = command.OtherAccountNumber,
            Description = command.Description,
            Comment = command.Comment,
            Tag = command.Tag,
            Amount = command.Amount,
            AmountAfterMutation = command.AmountAfterMutation,
            Account = await GetAccountAsync(command.UserId, command.AccountNumber),
            Book = await GetBookAsync(command.UserId, command.BookId),
        };

        entitie.Account.CurrentAmount += entitie.Amount;

        if (command.EventId.HasValue)
        {
            entitie.Event = await GetEventAsync(command.UserId, command.EventId.Value);
        }

        if (await MutionExistsAsync(entitie))
        {
            throw new ValidationException($"A mutation with these values already exists");
        }

        await _mutationRepository.InsertAsync(entitie);

        return entitie;
    }
}

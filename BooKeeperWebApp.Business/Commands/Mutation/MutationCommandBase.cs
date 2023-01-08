using BooKeeperWebApp.Infrastructure.Repositories;
using BooKeeperWebApp.Shared.Exceptions;

namespace BooKeeperWebApp.Business.Commands.Mutation;
public abstract class MutationCommandBase
{
    private readonly IGenericRepository<Infrastructure.Entities.Bank.BankAccount> _accountRepository;
    private readonly IGenericRepository<Infrastructure.Entities.Bank.Book> _bookRepository;
    private readonly IGenericRepository<Infrastructure.Entities.Bank.Event> _eventRepository;
    protected readonly IGenericRepository<Infrastructure.Entities.Bank.Mutation> _mutationRepository;

    protected MutationCommandBase(
        IGenericRepository<Infrastructure.Entities.Bank.BankAccount> accountRepository,
        IGenericRepository<Infrastructure.Entities.Bank.Book> bookRepository,
        IGenericRepository<Infrastructure.Entities.Bank.Event> eventRepository,
        IGenericRepository<Infrastructure.Entities.Bank.Mutation> mutationRepository)
    {
        _accountRepository = accountRepository;
        _bookRepository = bookRepository;
        _eventRepository = eventRepository;
        _mutationRepository = mutationRepository;
    }

    protected virtual async Task<Infrastructure.Entities.Bank.BankAccount> GetAccountAsync(Guid userId, string accountNumber)
    {
        var accounts = await _accountRepository.GetAsync(x => x.UserId == userId && x.Number!.ToLower().Equals(accountNumber.ToLower()), null, "MonthlyValues,YearlyValues");
        var account = accounts.FirstOrDefault() ?? throw new NotFoundException($"Bank account with number '{accountNumber}' not found.");

        return account;
    }

    protected virtual async Task<Infrastructure.Entities.Bank.Book> GetBookAsync(Guid userId, Guid bookId)
    {
        var books = await _bookRepository.GetAsync(x => x.UserId == userId && x.Id == bookId, null, "MonthlyValues,YearlyValues");
        var book = books.FirstOrDefault() ?? throw new NotFoundException($"Book with number '{bookId}' not found.");

        return book;
    }

    protected virtual async Task<Infrastructure.Entities.Bank.Event> GetEventAsync(Guid userId, Guid eventId)
    {
        var entitie = await _eventRepository.GetByIdAsync(eventId) ?? throw new NotFoundException($"Event with id '{eventId}' not found.");

        if (entitie.UserId != userId)
        {
            throw new UnauthorizedAccessException($"User with id '{userId}' does not have access to event with id '{eventId}'.");
        }

        return entitie;
    }

    protected virtual async Task<Infrastructure.Entities.Bank.Mutation> GetMutationAsync(Guid userId, Guid mutationId)
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

    protected virtual async Task<bool> MutionExistsAsync(Infrastructure.Entities.Bank.Mutation mutation)
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

    protected async Task<Infrastructure.Entities.Bank.Mutation> CreateMutation(AddMutationCommand command)
    {
        var entitie = new Infrastructure.Entities.Bank.Mutation
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

        UpdateAmountAndValues(entitie);

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

    private static void UpdateAmountAndValues(Infrastructure.Entities.Bank.Mutation entitie)
    {
        entitie.Account.CurrentAmount += entitie.Amount;

        UpdateAccountValues(entitie);

        UpdateBookValues(entitie);
    }

    private static void UpdateAccountValues(Infrastructure.Entities.Bank.Mutation entitie)
    {
        var monthlyValue = entitie.Account?.MonthlyValues?.FirstOrDefault(x => x.Date.Year == entitie.Date.Year && x.Date.Month == entitie.Date.Month);
        if (monthlyValue == null)
        {
            monthlyValue = new Infrastructure.Entities.MonthlyValue
            {
                AccountId = entitie.Account!.Id,
                Date = entitie.Date
            };

            entitie.Account.MonthlyValues!.Add(monthlyValue);
        }

        monthlyValue!.Value += entitie.Amount;

        var yearlyValue = entitie.Account?.YearlyValues?.FirstOrDefault(x => x.Year == entitie.Date.Year);
        if (yearlyValue == null)
        {
            yearlyValue = new Infrastructure.Entities.YearlyValue
            {
                AccountId = entitie.Account!.Id,
                Year = entitie.Date.Year
            };

            entitie.Account.YearlyValues!.Add(yearlyValue);
        }

        yearlyValue!.Value += entitie.Amount;
    }

    private static void UpdateBookValues(Infrastructure.Entities.Bank.Mutation entitie)
    {
        var monthlyValue = entitie.Book?.MonthlyValues?.FirstOrDefault(x => x.Date.Year == entitie.Date.Year && x.Date.Month == entitie.Date.Month);
        if (monthlyValue == null)
        {
            monthlyValue = new Infrastructure.Entities.MonthlyValue
            {
                AccountId = entitie.Account!.Id,
                Date = entitie.Date
            };

            entitie.Book!.MonthlyValues!.Add(monthlyValue);
        }

        monthlyValue!.Value += entitie.Amount;

        var yearlyValue = entitie.Book?.YearlyValues?.FirstOrDefault(x => x.Year == entitie.Date.Year);
        if (yearlyValue == null)
        {
            yearlyValue = new Infrastructure.Entities.YearlyValue
            {
                AccountId = entitie.Account!.Id,
                Year = entitie.Date.Year
            };

            entitie.Book!.YearlyValues!.Add(yearlyValue);
        }

        yearlyValue!.Value += entitie.Amount;
    }
}

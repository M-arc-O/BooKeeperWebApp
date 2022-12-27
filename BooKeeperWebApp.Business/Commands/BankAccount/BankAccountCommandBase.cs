using BooKeeperWebApp.Infrastructure.Repositories;
using BooKeeperWebApp.Shared.Exceptions;

namespace BooKeeperWebApp.Business.Commands.BankAccount;
public abstract class BankAccountCommandBase
{
    private readonly IGenericRepository<Infrastructure.Entities.Bank.BankAccount> _bankAccountRepository;

    protected BankAccountCommandBase(IGenericRepository<Infrastructure.Entities.Bank.BankAccount> bankAccountRepository)
    {
        _bankAccountRepository = bankAccountRepository;
    }

    protected virtual async Task<Infrastructure.Entities.Bank.BankAccount> GetBankAccountAsync(Guid userId, Guid accountId)
    {
        var bankAccount = await _bankAccountRepository.GetByIdAsync(accountId) ?? throw new NotFoundException($"Bank account with id '{accountId}' not found.");

        if (bankAccount.UserId != userId)
        {
            throw new UnauthorizedAccessException($"User with id '{userId}' does not have access to bank account with id '{accountId}'.");
        }

        return bankAccount;
    }

    protected virtual async Task<bool> NumberTakenAsync(string number)
    {
        var accounts = await _bankAccountRepository.GetAsync(x => x.Number!.ToLower().Equals(number.ToLower()));
        return accounts.Any();
    }
}

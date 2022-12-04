﻿using BooKeeperWebApp.Infrastructure.Repositories;
using BooKeeperWebApp.Shared.Exceptions;

namespace BooKeeperWebApp.Business.Commands.BankAccount;
public abstract class BankAccountCommandBase
{
    private readonly IGenericRepository<Infrastructure.Entities.BankAccount> _bankAccountRepository;

    protected BankAccountCommandBase(IGenericRepository<Infrastructure.Entities.BankAccount> bankAccountRepository)
    {
        _bankAccountRepository = bankAccountRepository;
    }

    protected virtual async Task<Infrastructure.Entities.BankAccount> GetBankAccount(Guid userId, Guid accountId)
    {
        var bankAccount = await _bankAccountRepository.GetByIdAsync(accountId) ?? throw new NotFoundException($"Bank account with id '{accountId}' not found.");

        if (bankAccount.UserId != userId)
        {
            throw new UnauthorizedAccessException($"User with id '{userId}' does not have access to bank account with id '{accountId}'.");
        }

        return bankAccount;
    }
}

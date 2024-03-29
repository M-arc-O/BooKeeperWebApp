﻿using AutoMapper;
using BooKeeperWebApp.Business.CQRS;
using BooKeeperWebApp.Business.Models.Bank;
using BooKeeperWebApp.Infrastructure.Enums;
using BooKeeperWebApp.Infrastructure.Repositories;
using BooKeeperWebApp.Shared.Exceptions;

namespace BooKeeperWebApp.Business.Commands.BankAccount;
public class UpdateBankAccountCommandHandler : BankAccountCommandBase, IHandler<UpdateBankAccountCommand, BankAccountModel>
{
    private readonly IMapper _mapper;

    public UpdateBankAccountCommandHandler(IGenericRepository<Infrastructure.Entities.Bank.BankAccount> bankAccountRepository, IMapper mapper)
        : base(bankAccountRepository)
    {
        _mapper = mapper;
    }

    public async Task<BankAccountModel> ExecuteAsync(UpdateBankAccountCommand command)
    {
        var bankAccount = await GetBankAccountAsync(command.UserId, command.AccountId);

        if (command.Number != bankAccount.Number && await NumberTakenAsync(command.UserId, command.Number))
        {
            throw new ValidationException($"Account with number '{command.Number}' already exists");
        }

        bankAccount.Name = command.Name;
        bankAccount.Number = command.Number;
        bankAccount.Type = (BankAccountType)command.Type;
        bankAccount.StartAmount = command.StartAmount;
        _bankAccountRepository.Update(bankAccount);

        return _mapper.Map<BankAccountModel>(bankAccount);
    }
}

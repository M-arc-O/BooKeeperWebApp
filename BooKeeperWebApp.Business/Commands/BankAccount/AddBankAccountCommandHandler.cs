using AutoMapper;
using BooKeeperWebApp.Business.CQRS;
using BooKeeperWebApp.Business.Models.Bank;
using BooKeeperWebApp.Infrastructure.Enums;
using BooKeeperWebApp.Infrastructure.Repositories;
using BooKeeperWebApp.Shared.Exceptions;

namespace BooKeeperWebApp.Business.Commands.BankAccount;
public class AddBankAccountCommandHandler : BankAccountCommandBase, IHandler<AddBankAccountCommand, BankAccountModel>
{
    private readonly IMapper _mapper;

    public AddBankAccountCommandHandler(IGenericRepository<Infrastructure.Entities.Bank.BankAccount> bankAccountRepository, IMapper mapper)
        : base(bankAccountRepository)
    {
        _mapper = mapper;
    }

    public async Task<BankAccountModel> ExecuteAsync(AddBankAccountCommand command)
    {
        var bankAccount = new Infrastructure.Entities.Bank.BankAccount
        {
            Id = Guid.NewGuid(),
            UserId = command.UserId,
            Name = command.Name,
            Number = command.Number,
            Type = (BankAccountType)command.Type,
            StartAmount = command.StartAmount,
            CurrentAmount = command.StartAmount
        };

        if (await NumberTakenAsync(command.Number))
        {
            throw new ValidationException($"Account with number '{command.Number}' already exists");
        }

        await _bankAccountRepository.InsertAsync(bankAccount);

        return _mapper.Map<BankAccountModel>(bankAccount);
    }
}

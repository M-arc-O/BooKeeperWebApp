using AutoMapper;
using BooKeeperWebApp.Business.CQRS;
using BooKeeperWebApp.Business.Models;
using BooKeeperWebApp.Infrastructure.Entities;
using BooKeeperWebApp.Infrastructure.Repositories;

namespace BooKeeperWebApp.Business.Commands;
public class AddBankAccountCommandHandler : IHandler<AddBankAccountCommand, BankAccountModel>
{
    private readonly IGenericRepository<BankAccount> _bankAccountRepository;
    private readonly IMapper _mapper;

    public AddBankAccountCommandHandler(IGenericRepository<BankAccount> bankAccountRepository, IMapper mapper)
    {
        _bankAccountRepository = bankAccountRepository;
        _mapper = mapper;
    }

    public async Task<BankAccountModel> ExecuteAsync(AddBankAccountCommand command)
    {
        var bankAccount = new BankAccount {
            Id = Guid.NewGuid(),
            UserId = command.UserId,
            Name = command.AccountName
        };
        await _bankAccountRepository.InsertAsync(bankAccount);

        return _mapper.Map< BankAccountModel>(bankAccount);
    }
}

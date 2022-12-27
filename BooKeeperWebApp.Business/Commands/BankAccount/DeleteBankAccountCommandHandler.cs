using BooKeeperWebApp.Business.CQRS;
using BooKeeperWebApp.Infrastructure.Repositories;

namespace BooKeeperWebApp.Business.Commands.BankAccount;
public class DeleteBankAccountCommandHandler : BankAccountCommandBase, IHandler<DeleteBankAccountCommand, Guid>
{
    private readonly IGenericRepository<Infrastructure.Entities.Bank.BankAccount> _bankAccountRepository;

    public DeleteBankAccountCommandHandler(IGenericRepository<Infrastructure.Entities.Bank.BankAccount> bankAccountRepository)
        : base(bankAccountRepository)
    {
        _bankAccountRepository = bankAccountRepository;
    }

    public async Task<Guid> ExecuteAsync(DeleteBankAccountCommand command)
    {
        var bankAccount = await GetBankAccountAsync(command.UserId, command.AccountId);
        _bankAccountRepository.Delete(bankAccount);

        return command.AccountId;
    }
}

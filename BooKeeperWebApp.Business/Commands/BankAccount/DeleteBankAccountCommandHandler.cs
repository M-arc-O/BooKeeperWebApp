using BooKeeperWebApp.Business.CQRS;
using BooKeeperWebApp.Infrastructure.Repositories;

namespace BooKeeperWebApp.Business.Commands.BankAccount;
public class DeleteBankAccountCommandHandler : BankAccountCommandBase, IHandler<DeleteBankAccountCommand, Guid>
{
    private readonly IGenericRepository<Infrastructure.Entities.BankAccount> _bankAccountRepository;

    public DeleteBankAccountCommandHandler(IGenericRepository<Infrastructure.Entities.BankAccount> bankAccountRepository)
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

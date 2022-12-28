using BooKeeperWebApp.Business.CQRS;
using BooKeeperWebApp.Infrastructure.Repositories;
using BooKeeperWebApp.Shared.Exceptions;

namespace BooKeeperWebApp.Business.Commands.BankAccount;
public class DeleteBankAccountCommandHandler : BankAccountCommandBase, IHandler<DeleteBankAccountCommand, Guid>
{
    private readonly IGenericRepository<Infrastructure.Entities.Bank.Mutation> _mutationRepository;

    public DeleteBankAccountCommandHandler(
        IGenericRepository<Infrastructure.Entities.Bank.BankAccount> bankAccountRepository,
        IGenericRepository<Infrastructure.Entities.Bank.Mutation> mutationRepository)
        : base(bankAccountRepository)
    {
        _mutationRepository = mutationRepository;
    }

    public async Task<Guid> ExecuteAsync(DeleteBankAccountCommand command)
    {
        var bankAccounts = await _bankAccountRepository.GetAsync(x => x.Id == command.AccountId && x.UserId == command.UserId, null, "Mutations");
        var bankAccount = bankAccounts.FirstOrDefault() ?? throw new NotFoundException($"Bank account with id '{command.AccountId}' not found.");

        Parallel.ForEach(bankAccount.Mutations!, (mutation) =>
        {
            _mutationRepository.Delete(mutation);
        });

        _bankAccountRepository.Delete(bankAccount);

        return command.AccountId;
    }
}

using BooKeeperWebApp.Business.CQRS;
using BooKeeperWebApp.Infrastructure.Repositories;
using BooKeeperWebApp.Shared.Exceptions;

namespace BooKeeperWebApp.Business.Commands.BankAccount;
public class DeleteBankAccountCommandHandler : BankAccountCommandBase, IHandler<DeleteBankAccountCommand, Guid>
{
    private readonly IGenericRepository<Infrastructure.Entities.Bank.Mutation> _mutationRepository;
    private readonly IGenericRepository<Infrastructure.Entities.MonthlyValue> _monthlyValueRepository;
    private readonly IGenericRepository<Infrastructure.Entities.YearlyValue> _yearlyValueRepository;

    public DeleteBankAccountCommandHandler(
        IGenericRepository<Infrastructure.Entities.Bank.BankAccount> bankAccountRepository,
        IGenericRepository<Infrastructure.Entities.Bank.Mutation> mutationRepository,
        IGenericRepository<Infrastructure.Entities.MonthlyValue> monthlyValueRepository,
        IGenericRepository<Infrastructure.Entities.YearlyValue> yearlyValueRepository)
        : base(bankAccountRepository)
    {
        _mutationRepository = mutationRepository;
        _monthlyValueRepository = monthlyValueRepository;
        _yearlyValueRepository = yearlyValueRepository;
    }

    public async Task<Guid> ExecuteAsync(DeleteBankAccountCommand command)
    {
        var bankAccounts = await _bankAccountRepository.GetAsync(x => x.Id == command.AccountId && x.UserId == command.UserId, null, "Mutations,MonthlyValues,YearlyValues");
        var bankAccount = bankAccounts.FirstOrDefault() ?? throw new NotFoundException($"Bank account with id '{command.AccountId}' not found.");

        foreach (var mutation in bankAccount.Mutations!)
        {
            _mutationRepository.Delete(mutation);
        }

        foreach (var monlthlyValue in bankAccount.MonthlyValues!)
        {
            _monthlyValueRepository.Delete(monlthlyValue);
        }

        foreach (var yearlyValue in bankAccount.YearlyValues!)
        {
            _yearlyValueRepository.Delete(yearlyValue);
        }

        _bankAccountRepository.Delete(bankAccount);

        return command.AccountId;
    }
}

using BooKeeperWebApp.Business.CQRS;
using BooKeeperWebApp.Infrastructure.Entities.Bank;
using BooKeeperWebApp.Infrastructure.Repositories;
using BooKeeperWebApp.Shared.Exceptions;

namespace BooKeeperWebApp.Business.Commands.InvestmentAccount;
public class DeleteInvestmentAccountCommandHandler : InvestmentAccountCommandBase, IHandler<DeleteInvestmentAccountCommand, Guid>
{
    private readonly IGenericRepository<Infrastructure.Entities.Investment.Investment> _investmentRepository;
    private readonly IGenericRepository<Infrastructure.Entities.Investment.InvestmentValue> _investmentValueRepository;
    private readonly IGenericRepository<Infrastructure.Entities.MonthlyValue> _monthlyValueRepository;
    private readonly IGenericRepository<Infrastructure.Entities.YearlyValue> _yearlyValueRepository;

    public DeleteInvestmentAccountCommandHandler(
        IGenericRepository<Infrastructure.Entities.Investment.InvestmentAccount> investmentAccountRepository,
        IGenericRepository<Infrastructure.Entities.Investment.Investment> investmentRepository,
        IGenericRepository<Infrastructure.Entities.Investment.InvestmentValue> investmentValueRepository,
        IGenericRepository<Infrastructure.Entities.MonthlyValue> monthlyValueRepository,
        IGenericRepository<Infrastructure.Entities.YearlyValue> yearlyValueRepository)
        : base(investmentAccountRepository)
    {
        _investmentRepository = investmentRepository;
        _investmentValueRepository = investmentValueRepository;
        _monthlyValueRepository = monthlyValueRepository;
        _yearlyValueRepository = yearlyValueRepository;
    }

    public async Task<Guid> ExecuteAsync(DeleteInvestmentAccountCommand command)
    {
        var investmentAccounts = await _investmentAccountRepository.GetAsync(
            x => x.Id == command.AccountId && x.UserId == command.UserId,
            null,
            "Investments,Investments.Values,MonthlyValues,YearlyValues");
        var investmentAccount = investmentAccounts.FirstOrDefault() ?? throw new NotFoundException($"Investment account with id '{command.AccountId}' not found.");

        foreach (var investment in investmentAccount.Investments!)
        {
            foreach (var value in investment.Values)
            {
                _investmentValueRepository.Delete(value);
            }
            _investmentRepository.Delete(investment);
        }

        foreach (var monlthlyValue in investmentAccount.MonthlyValues!)
        {
            _monthlyValueRepository.Delete(monlthlyValue);
        }

        foreach (var yearlyValue in investmentAccount.YearlyValues!)
        {
            _yearlyValueRepository.Delete(yearlyValue);
        }

        _investmentAccountRepository.Delete(investmentAccount);

        return command.AccountId;
    }
}

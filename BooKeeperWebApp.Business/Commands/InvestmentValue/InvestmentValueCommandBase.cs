using BooKeeperWebApp.Infrastructure.Repositories;
using BooKeeperWebApp.Shared.Exceptions;

namespace BooKeeperWebApp.Business.Commands.InvestmentValue;
public abstract class InvestmentValueCommandBase
{
    protected readonly IGenericRepository<Infrastructure.Entities.Investment.InvestmentValue> _investmentValueRepository;
    protected readonly IGenericRepository<Infrastructure.Entities.Investment.Investment> _investmentRepository;
    protected readonly IGenericRepository<Infrastructure.Entities.Investment.InvestmentAccount> _investmentAccountRepository;

    protected InvestmentValueCommandBase(
        IGenericRepository<Infrastructure.Entities.Investment.InvestmentValue> investmentValueRepository,
        IGenericRepository<Infrastructure.Entities.Investment.Investment> investmentRepository,
        IGenericRepository<Infrastructure.Entities.Investment.InvestmentAccount> investmentAccountRepository)
    {
        _investmentValueRepository = investmentValueRepository;
        _investmentRepository = investmentRepository;
        _investmentAccountRepository= investmentAccountRepository;
    }

    protected virtual async Task<Infrastructure.Entities.Investment.InvestmentValue> GetInvestmentValueAsync(Guid userId, Guid valueId)
    {
        var investmentValue = await _investmentValueRepository.GetByIdAsync(valueId) 
            ?? throw new NotFoundException($"Investment value with id '{valueId}' not found.");

        var investment = await _investmentRepository.GetByIdAsync(investmentValue.InvestmentId);
        var investmentAccount = await _investmentAccountRepository.GetByIdAsync(investment!.InvestmentAccountId);

        if (investmentAccount!.UserId != userId)
        {
            throw new UnauthorizedAccessException($"User with id '{userId}' does not have access to investment value with id '{valueId}'.");
        }

        return investmentValue;
    }

    protected virtual async Task<bool> DateTakenAsync(DateTime date)
    {
        var accounts = await _investmentValueRepository.GetAsync(x => x.Date.Date == date.Date);
        return accounts.Any();
    }
}

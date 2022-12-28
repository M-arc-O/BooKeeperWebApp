using BooKeeperWebApp.Infrastructure.Repositories;
using BooKeeperWebApp.Shared.Exceptions;

namespace BooKeeperWebApp.Business.Commands.Investment;
public abstract class InvestmentCommandBase
{
    protected readonly IGenericRepository<Infrastructure.Entities.Investment.Investment> _investmentRepository;
    protected readonly IGenericRepository<Infrastructure.Entities.Investment.InvestmentAccount> _investmentAccountRepository;

    protected InvestmentCommandBase(
        IGenericRepository<Infrastructure.Entities.Investment.Investment> investmentRepository, 
        IGenericRepository<Infrastructure.Entities.Investment.InvestmentAccount> investmentAccountRepository)
    {
        _investmentRepository = investmentRepository;
        _investmentAccountRepository = investmentAccountRepository;
    }

    protected virtual async Task<Infrastructure.Entities.Investment.Investment> GetInvestmentAsync(Guid userId, Guid investmentId)
    {
        var investment = await _investmentRepository.GetByIdAsync(investmentId)
            ?? throw new NotFoundException($"Investment with id '{investmentId}' not found.");

        await CheckIfUserHasAccesToInvestment(userId, investment);

        return investment;
    }

    protected async Task CheckIfUserHasAccesToInvestment(Guid userId, Infrastructure.Entities.Investment.Investment investment)
    {
        var account = await _investmentAccountRepository.GetByIdAsync(investment.InvestmentAccountId);

        if (account!.UserId != userId)
        {
            throw new UnauthorizedAccessException($"User with id '{userId}' does not have access to investment with id '{investment.Id}'.");
        }
    }

    protected virtual async Task<bool> NameTakenAsync(string name)
    {
        var accounts = await _investmentRepository.GetAsync(x => x.Name!.ToLower().Equals(name.ToLower()));
        return accounts.Any();
    }
}

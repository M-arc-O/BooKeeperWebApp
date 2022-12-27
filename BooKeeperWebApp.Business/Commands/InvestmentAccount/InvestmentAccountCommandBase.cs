using BooKeeperWebApp.Infrastructure.Repositories;
using BooKeeperWebApp.Shared.Exceptions;

namespace BooKeeperWebApp.Business.Commands.InvestmentAccount;
public abstract class InvestmentAccountCommandBase
{
    private readonly IGenericRepository<Infrastructure.Entities.Investment.InvestmentAccount> _investmentAccountRepository;

    protected InvestmentAccountCommandBase(IGenericRepository<Infrastructure.Entities.Investment.InvestmentAccount> investmentAccountRepository)
    {
        _investmentAccountRepository = investmentAccountRepository;
    }

    protected virtual async Task<Infrastructure.Entities.Investment.InvestmentAccount> GetInvestmentAccountAsync(Guid userId, Guid accountId)
    {
        var investmentAccount = await _investmentAccountRepository.GetByIdAsync(accountId) 
            ?? throw new NotFoundException($"Investment account with id '{accountId}' not found.");

        if (investmentAccount.UserId != userId)
        {
            throw new UnauthorizedAccessException($"User with id '{userId}' does not have access to investment account with id '{accountId}'.");
        }

        return investmentAccount;
    }

    protected virtual async Task<bool> NameTakenAsync(string name)
    {
        var accounts = await _investmentAccountRepository.GetAsync(x => x.Name!.ToLower().Equals(name.ToLower()));
        return accounts.Any();
    }
}

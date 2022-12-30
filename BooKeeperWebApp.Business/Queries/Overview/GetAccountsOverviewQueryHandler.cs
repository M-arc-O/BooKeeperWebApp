using BooKeeperWebApp.Business.CQRS;
using BooKeeperWebApp.Business.Models.Overview;
using BooKeeperWebApp.Infrastructure.Repositories;

namespace BooKeeperWebApp.Business.Queries.Overview;
public class GetAccountsOverviewQueryHandler : IHandler<GetAccountsOverviewQuery, IEnumerable<OverviewAccountModel>>
{
    private readonly IGenericRepository<Infrastructure.Entities.Bank.BankAccount> _bankAccountRepository;
    private readonly IGenericRepository<Infrastructure.Entities.Investment.InvestmentAccount> _investmentAccountRepository;

    public GetAccountsOverviewQueryHandler(
        IGenericRepository<Infrastructure.Entities.Bank.BankAccount> accountRepository,
        IGenericRepository<Infrastructure.Entities.Investment.InvestmentAccount> investmentAccountRepository)
    {
        _bankAccountRepository = accountRepository;
        _investmentAccountRepository = investmentAccountRepository;
    }

    public async Task<IEnumerable<OverviewAccountModel>> ExecuteAsync(GetAccountsOverviewQuery query)
    {
        var bankAccounts = await _bankAccountRepository.GetAsync(x => x.UserId == query.UserId);
        var investmentAccounts = await _investmentAccountRepository.GetAsync(x => x.UserId == query.UserId);

        var retVal = new List<OverviewAccountModel>();

        foreach (var account in bankAccounts)
        {
            var overviewAccount = new OverviewAccountModel
            {
                AccountName = account.Name!,
                Amount = account.CurrentAmount
            };
            retVal.Add(overviewAccount);
        }

        foreach (var account in investmentAccounts)
        {
            var overviewAccount = new OverviewAccountModel
            {
                AccountName = account.Name!,
                Amount = account.CurrentAmount
            };
            retVal.Add(overviewAccount);
        }

        return retVal;
    }
}

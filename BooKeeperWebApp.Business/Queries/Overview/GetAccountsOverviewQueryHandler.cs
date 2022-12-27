using BooKeeperWebApp.Business.CQRS;
using BooKeeperWebApp.Business.Models.Overview;
using BooKeeperWebApp.Infrastructure.Repositories;

namespace BooKeeperWebApp.Business.Queries.Overview;
public class GetAccountsOverviewQueryHandler : IHandler<GetAccountsOverviewQuery, IEnumerable<OverviewAccountModel>>
{
    private readonly IGenericRepository<Infrastructure.Entities.Bank.BankAccount> _accountRepository;

    public GetAccountsOverviewQueryHandler(IGenericRepository<Infrastructure.Entities.Bank.BankAccount> accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task<IEnumerable<OverviewAccountModel>> ExecuteAsync(GetAccountsOverviewQuery query)
    {
        var accounts = await _accountRepository.GetAsync(x => x.UserId == query.UserId);

        var retVal = new List<OverviewAccountModel>();

        Parallel.ForEach(accounts, account =>
        {
            var overviewBook = new OverviewAccountModel
            {
                AccountName = account.Name!,
                Amount = account.CurrentAmount
            };
            retVal.Add(overviewBook);
        });

        return retVal;
    }
}

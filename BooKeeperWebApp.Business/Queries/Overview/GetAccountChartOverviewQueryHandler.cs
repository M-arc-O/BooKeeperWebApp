using BooKeeperWebApp.Business.CQRS;
using BooKeeperWebApp.Business.Models.Overview;
using BooKeeperWebApp.Infrastructure.Repositories;

namespace BooKeeperWebApp.Business.Queries.Overview;
public class GetAccountChartOverviewQueryHandler : IHandler<GetAccountChartOverviewQuery, IEnumerable<OverviewDateValueModel>>
{
    private readonly IGenericRepository<Infrastructure.Entities.Bank.BankAccount> _bankAccountRepository;
    private readonly IGenericRepository<Infrastructure.Entities.Investment.InvestmentAccount> _investmentAccountRepository;

    public GetAccountChartOverviewQueryHandler(
        IGenericRepository<Infrastructure.Entities.Bank.BankAccount> accountRepository,
        IGenericRepository<Infrastructure.Entities.Investment.InvestmentAccount> investmentAccountRepository)
    {
        _bankAccountRepository = accountRepository;
        _investmentAccountRepository = investmentAccountRepository;
    }

    public async Task<IEnumerable<OverviewDateValueModel>> ExecuteAsync(GetAccountChartOverviewQuery query)
    {
        var retVal = new List<OverviewDateValueModel>();

        var investmentAccounts = await _investmentAccountRepository.GetAsync(x => x.UserId == query.UserId, null, "Investments,Investments.Values");
        var account = investmentAccounts.FirstOrDefault();

        if (account == null || account.Investments == null)
        {
            return retVal;
        }

        for(int i = 1; i <= 12; i++)
        {
            var amount = 0.0;

            foreach (var investment in account!.Investments!)
            {
                var value = investment.Values.FirstOrDefault(x=>x.Date.Year == 2022 && x.Date.Month == i);
                if (value != null)
                {
                    amount += value.Value;
                }

            }
            
            retVal.Add(new OverviewDateValueModel
            {
                Date = new DateTime(2022, i, 1),
                Value = amount
            });
        }

        return retVal;
    }
}

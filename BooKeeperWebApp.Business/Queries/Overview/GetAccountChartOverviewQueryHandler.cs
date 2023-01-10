using BooKeeperWebApp.Business.CQRS;
using BooKeeperWebApp.Business.Models.Overview;
using BooKeeperWebApp.Infrastructure.Entities;
using BooKeeperWebApp.Infrastructure.Repositories;
using BooKeeperWebApp.Shared.Enums;
using System.Security.Principal;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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


        if (query.AccountId == Guid.Empty)
        {
            var investmentAccounts = await _investmentAccountRepository.GetAsync(x => x.UserId == query.UserId, null, "Investments,Investments.Values");
            foreach (var account in investmentAccounts)
            {
                var values = HandleInvestmentAccount(account, query.TimespanType, query.NumberOf);
                HandleValues(retVal, values);
            }

            var bankAccounts = new List<Infrastructure.Entities.Bank.BankAccount>();
            bankAccounts = query.TimespanType switch
            {
                TimespanType.Years => await _bankAccountRepository.GetAsync(x => x.UserId == query.UserId, null, "YearlyValues"),
                TimespanType.Months => await _bankAccountRepository.GetAsync(x => x.UserId == query.UserId, null, "MonthlyValues"),
                _ => throw new Exception("Unkown TimespanType"),
            };
            foreach (var account in bankAccounts)
            {
                var values = HandleBankAccount(account, query.TimespanType, query.NumberOf);
                HandleValues(retVal, values);
            }
        }
        else
        {
            var investmentAccounts = await _investmentAccountRepository.GetAsync(x => x.Id == query.AccountId && x.UserId == query.UserId, null, "Investments,Investments.Values");
            if (investmentAccounts.Count > 0)
            {
                var account = investmentAccounts.FirstOrDefault();

                if (account == null || account.Investments == null)
                {
                    return retVal;
                }

                var values = HandleInvestmentAccount(account, query.TimespanType, query.NumberOf);
                retVal.AddRange(values);
            }
            else
            {
                var accounts = new List<Infrastructure.Entities.Bank.BankAccount>();
                accounts = query.TimespanType switch
                {
                    TimespanType.Years => await _bankAccountRepository.GetAsync(x => x.Id == query.AccountId && x.UserId == query.UserId, null, "YearlyValues"),
                    TimespanType.Months => await _bankAccountRepository.GetAsync(x => x.Id == query.AccountId && x.UserId == query.UserId, null, "MonthlyValues"),
                    _ => throw new Exception("Unkown TimespanType"),
                };
                var account = accounts.FirstOrDefault();

                if (account == null)
                {
                    return retVal;
                }

                var values = HandleBankAccount(account, query.TimespanType, query.NumberOf);
                retVal.AddRange(values);
            }
        }

        return retVal;
    }

    private static List<OverviewDateValueModel> HandleInvestmentAccount(
        Infrastructure.Entities.Investment.InvestmentAccount account,
        TimespanType timespanType,
        int numberOf)
    {
        var retVal = new List<OverviewDateValueModel>();

        switch (timespanType)
        {
            case TimespanType.Years:
                for (int i = 0; i < numberOf; i++)
                {
                    var amount = 0.0;
                    var date = DateTime.Now.AddYears(-1 * i);

                    foreach (var investment in account!.Investments!)
                    {
                        var value = investment.Values.OrderByDescending(x => x.Date).FirstOrDefault(x => x.Date.Year == date.Year);
                        if (value != null)
                        {
                            amount += value.Value;
                        }

                    }

                    retVal.Add(new OverviewDateValueModel
                    {
                        Date = date.Date,
                        Value = amount
                    });
                }
                break;
            case TimespanType.Months:
                for (int i = 0; i < numberOf; i++)
                {
                    var amount = 0.0;
                    var date = DateTime.Now.AddMonths(-1 * i);

                    foreach (var investment in account!.Investments!)
                    {
                        var value = investment.Values.OrderByDescending(x => x.Date).FirstOrDefault(x => x.Date.Year == date.Year && x.Date.Month == date.Month);
                        if (value != null)
                        {
                            amount += value.Value;
                        }

                    }

                    retVal.Add(new OverviewDateValueModel
                    {
                        Date = date.Date,
                        Value = amount
                    });
                }
                break;
            default:
                throw new Exception("Unkown TimespanType");
        }

        return retVal;
    }

    private static List<OverviewDateValueModel> HandleBankAccount(
        Infrastructure.Entities.Bank.BankAccount account,
        TimespanType timespanType,
        int numberOf)
    {
        var retVal = new List<OverviewDateValueModel>();

        switch (timespanType)
        {
            case TimespanType.Years:
                for (int i = 0; i < numberOf; i++)
                {
                    var amount = 0.0;
                    var date = DateTime.Now.AddYears(-1 * i);

                    var value = account!.YearlyValues!.FirstOrDefault(x => x.Year == date.Year);
                    if (value != null)
                    {
                        amount += value.Value;
                    }

                    retVal.Add(new OverviewDateValueModel
                    {
                        Date = date.Date,
                        Value = amount
                    });
                }
                break;
            case TimespanType.Months:
                for (int i = 0; i < numberOf; i++)
                {
                    var amount = 0.0;
                    var date = DateTime.Now.AddMonths(-1 * i);

                    var value = account!.MonthlyValues!.FirstOrDefault(x => x.Date.Year == date.Year && x.Date.Month == date.Month);
                    if (value != null)
                    {
                        amount += value.Value;
                    }

                    retVal.Add(new OverviewDateValueModel
                    {
                        Date = date.Date,
                        Value = amount
                    });
                }
                break;
            default:
                throw new Exception("Unkown TimespanType");
        }

        return retVal;
    }

    private static void HandleValues(List<OverviewDateValueModel> retVal, List<OverviewDateValueModel> values)
    {
        foreach (var value in values)
        {
            var retValValue = retVal.FirstOrDefault(x => x.Date == value.Date);

            if (retValValue == null)
            {
                retVal.Add(value);
            }
            else
            {
                retValValue.Value += value.Value;
            }
        }
    }
}

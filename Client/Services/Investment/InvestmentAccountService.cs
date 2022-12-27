using BooKeeperWebApp.Shared.Dtos.Investment;
using BooKeeperWebApp.Shared.Models.Investment;
using Radzen;

namespace Client.Services.Investment;

public class InvestmentAccountService : HttpServiceBase<InvestmentAccountDto, AddInvestmentAccountModel>
{
    public InvestmentAccountService(HttpClient httpClient, NotificationService notificationService)
        : base(httpClient, notificationService, "/api/investmentaccount/")
    {

    }

    public async Task<bool> CreateInvestmentAccountAsync(InvestmentAccountDto account)
    {
        var investmentAccount = new AddInvestmentAccountModel(account.Name!, account.Type);
        return await CreateAsync(investmentAccount);
    }

    public async Task<bool> UpdateInvestmentAccountAsync(InvestmentAccountDto account)
    {
        var investmentAccount = new AddInvestmentAccountModel(account.Name!, account.Type);
        return await UpdateAsync(investmentAccount, account.Id);
    }
}

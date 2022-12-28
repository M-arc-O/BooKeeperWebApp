using BooKeeperWebApp.Shared.Dtos.Investment;
using BooKeeperWebApp.Shared.Models.Investment;
using Radzen;

namespace Client.Services.Investment;

public class InvestmentValueService : HttpServiceBase<InvestmentValueDto, AddInvestmentValueModel>
{
    public InvestmentValueService(HttpClient httpClient, NotificationService notificationService)
        : base(httpClient, notificationService, "/api/investmentvalue/")
    {

    }

    public async Task<bool> CreateInvestmentAccountAsync(InvestmentValueDto value)
    {
        var investmentValue = new AddInvestmentValueModel(value.Date, value.Value);
        return await CreateAsync(investmentValue);
    }

    public async Task<bool> UpdateInvestmentAccountAsync(InvestmentValueDto value)
    {
        var investmentValue = new AddInvestmentValueModel(value.Date, value.Value);
        return await UpdateAsync(investmentValue, value.Id);
    }
}

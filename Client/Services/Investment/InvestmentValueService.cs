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

    public async Task<bool> CreateInvestmentValueAsync(Guid investmentId, DateTime date, double value)
    {
        var investmentValue = new AddInvestmentValueModel(investmentId, date, value);
        return await CreateAsync(investmentValue);
    }
}

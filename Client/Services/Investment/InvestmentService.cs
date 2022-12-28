using BooKeeperWebApp.Shared.Dtos.Investment;
using BooKeeperWebApp.Shared.Models.Investment;
using Radzen;

namespace Client.Services.Investment;

public class InvestmentService : HttpServiceBase<InvestmentDto, AddInvestmentModel>
{
    public InvestmentService(HttpClient httpClient, NotificationService notificationService)
        : base(httpClient, notificationService, "/api/investment/")
    {

    }

    public async Task<bool> CreateInvestmentAsync(Guid investmentAccountId, string name)
    {
        var investment = new AddInvestmentModel(investmentAccountId, name);
        return await CreateAsync(investment);
    }

    public async Task<bool> UpdateInvestmentAsync(Guid investmentAccountId, InvestmentDto investementDto)
    {
        var investment = new AddInvestmentModel(investmentAccountId, investementDto.Name);
        return await UpdateAsync(investment, investementDto.Id);
    }
}

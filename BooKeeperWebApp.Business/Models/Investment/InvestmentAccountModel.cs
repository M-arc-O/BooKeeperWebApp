using BooKeeperWebApp.Shared.Enums;

namespace BooKeeperWebApp.Business.Models.Investment;
public class InvestmentAccountModel
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string? Name { get; set; }
    public InvestmentAccountType Type { get; set; }
    public double CurrentAmount { get; set; }
    public ICollection<InvestmentModel>? Investments { get; set; }
}

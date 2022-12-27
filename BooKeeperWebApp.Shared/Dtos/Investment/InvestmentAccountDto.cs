using BooKeeperWebApp.Shared.Enums;

namespace BooKeeperWebApp.Shared.Dtos.Investment;
public class InvestmentAccountDto : IBaseDto
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public InvestmentAccountType Type { get; set; }
    public double CurrentAmount { get; set; }
    public ICollection<InvestmentDto>? Investments { get; set; }
}
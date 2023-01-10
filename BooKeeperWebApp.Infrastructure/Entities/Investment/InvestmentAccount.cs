using BooKeeperWebApp.Infrastructure.Enums;

namespace BooKeeperWebApp.Infrastructure.Entities.Investment;
public  class InvestmentAccount
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string? Name { get; set; }
    public InvestmentAccountType Type { get; set; }
    public double CurrentAmount { get; set; }
    public ICollection<Investment>? Investments { get; set; }
}

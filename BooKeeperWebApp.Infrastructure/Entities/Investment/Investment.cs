namespace BooKeeperWebApp.Infrastructure.Entities.Investment;
public class Investment
{
    public Guid Id { get; set; }
    public Guid InvestmentAccountId { get; set; }
    public string Name { get; set; }
    public ICollection<InvestmentValue> Values { get; set; }
}

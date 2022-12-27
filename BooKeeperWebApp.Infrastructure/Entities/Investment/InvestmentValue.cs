namespace BooKeeperWebApp.Infrastructure.Entities.Investment;
public class InvestmentValue
{
    public Guid Id { get; set; }
    public Guid InvestmentId { get; set; }
    public DateTime Date { get; set; }
    public double Value { get; set; }
}

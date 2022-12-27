namespace BooKeeperWebApp.Business.Models.Investment;
public class InvestmentValueModel
{
    public Guid Id { get; set; }
    public Guid InvestmentId { get; set; }
    public DateTime Date { get; set; }
    public double Value { get; set; }
}

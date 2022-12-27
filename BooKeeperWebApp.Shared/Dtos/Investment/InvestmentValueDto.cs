namespace BooKeeperWebApp.Shared.Dtos.Investment;
public class InvestmentValueDto : IBaseDto
{
    public Guid Id { get; set; }
    public DateTime Date { get; set; }
    public double Value { get; set; }
}

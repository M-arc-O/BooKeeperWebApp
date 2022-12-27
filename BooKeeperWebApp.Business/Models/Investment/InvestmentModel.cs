namespace BooKeeperWebApp.Business.Models.Investment;
public class InvestmentModel
{
    public Guid Id { get; set; }
    public Guid InvestmentAccountId { get; set; }
    public string Name { get; set; }
    public ICollection<InvestmentValueModel> Values { get; set; }
}

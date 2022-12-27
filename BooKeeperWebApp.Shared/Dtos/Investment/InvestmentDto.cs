namespace BooKeeperWebApp.Shared.Dtos.Investment;
public class InvestmentDto : IBaseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public ICollection<InvestmentValueDto> Values { get; set; }
}

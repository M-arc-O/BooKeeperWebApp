using BooKeeperWebApp.Business.CQRS;

namespace BooKeeperWebApp.Business.Commands.InvestmentValue;
public class AddInvestmentValueCommand : ICommand
{
    public Guid UserId { get; }
    public Guid InvestmentId { get; set; }
    public DateTime Date { get; set; }
    public double Value { get; set; }

    public AddInvestmentValueCommand(Guid userId, Guid investmentId, DateTime date, double value)
    {
        UserId = userId;
        InvestmentId = investmentId;
        Date = date;
        Value = value;
    }
}

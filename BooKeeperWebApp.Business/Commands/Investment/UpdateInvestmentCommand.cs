using BooKeeperWebApp.Business.CQRS;

namespace BooKeeperWebApp.Business.Commands.Investment;
public class UpdateInvestmentCommand : ICommand
{
    public Guid UserId { get; }
    public Guid InvestmentId { get; }
    public string Name { get; }

    public UpdateInvestmentCommand(Guid userId, Guid investmentId, string name)
    {
        UserId = userId;
        InvestmentId = investmentId;
        Name = name;
    }
}

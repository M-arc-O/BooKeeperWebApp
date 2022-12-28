using BooKeeperWebApp.Business.CQRS;

namespace BooKeeperWebApp.Business.Commands.Investment;
public class DeleteInvestmentCommand : ICommand
{
    public Guid UserId { get; }
    public Guid InvestmentId { get; }

    public DeleteInvestmentCommand(Guid userId, Guid investmentId)
    {
        UserId = userId;
        InvestmentId = investmentId;
    }
}

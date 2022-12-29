using BooKeeperWebApp.Business.CQRS;

namespace BooKeeperWebApp.Business.Commands.InvestmentValue;
public class DeleteInvestmentValueCommand : ICommand
{
    public Guid UserId { get; }
    public Guid InvestmentValuetId { get; }

    public DeleteInvestmentValueCommand(Guid userId, Guid investmentValueId)
    {
        UserId = userId;
        InvestmentValuetId = investmentValueId;
    }
}

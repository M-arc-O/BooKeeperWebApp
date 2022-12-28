using BooKeeperWebApp.Business.CQRS;

namespace BooKeeperWebApp.Business.Commands.Investment;
public class AddInvestmentCommand : ICommand
{
    public Guid UserId { get; }
    public Guid InvestmentAccountId { get; }
    public string Name { get; }

    public AddInvestmentCommand(Guid userId, Guid investmentAccountId, string name)
    {
        UserId = userId;
        InvestmentAccountId = investmentAccountId;
        Name = name;
    }
}

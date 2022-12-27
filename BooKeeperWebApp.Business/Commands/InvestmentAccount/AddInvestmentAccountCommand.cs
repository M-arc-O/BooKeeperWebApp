using BooKeeperWebApp.Business.CQRS;
using BooKeeperWebApp.Shared.Enums;

namespace BooKeeperWebApp.Business.Commands.InvestmentAccount;
public class AddInvestmentAccountCommand : ICommand
{
    public Guid UserId { get; }
    public string Name { get; }
    public InvestmentAccountType Type { get; }

    public AddInvestmentAccountCommand(Guid userId, string name, InvestmentAccountType type)
    {
        UserId = userId;
        Name = name;
        Type = type;
    }
}

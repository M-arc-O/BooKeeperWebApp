using BooKeeperWebApp.Business.CQRS;
using BooKeeperWebApp.Shared.Enums;

namespace BooKeeperWebApp.Business.Commands.InvestmentAccount;
public class UpdateInvestmentAccountCommand : ICommand
{
    public Guid UserId { get; }
    public Guid AccountId { get; }
    public string Name { get; }
    public InvestmentAccountType Type { get; }

    public UpdateInvestmentAccountCommand(Guid userId, Guid accountId, string name, InvestmentAccountType type)
    {
        UserId = userId;
        AccountId = accountId;
        Name = name;
        Type = type;
    }
}

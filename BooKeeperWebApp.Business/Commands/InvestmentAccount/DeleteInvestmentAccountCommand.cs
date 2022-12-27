using BooKeeperWebApp.Business.CQRS;

namespace BooKeeperWebApp.Business.Commands.InvestmentAccount;
public class DeleteInvestmentAccountCommand : ICommand
{
    public Guid UserId { get; }
    public Guid AccountId { get; }

    public DeleteInvestmentAccountCommand(Guid userId, Guid accountId)
    {
        UserId = userId;
        AccountId = accountId;
    }
}

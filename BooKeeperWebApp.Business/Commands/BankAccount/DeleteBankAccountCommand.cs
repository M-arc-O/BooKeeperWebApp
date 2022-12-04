using BooKeeperWebApp.Business.CQRS;

namespace BooKeeperWebApp.Business.Commands.BankAccount;
public class DeleteBankAccountCommand : ICommand
{
    public Guid UserId { get; }
    public Guid AccountId { get; }

    public DeleteBankAccountCommand(Guid userId, Guid accountId)
    {
        UserId = userId;
        AccountId = accountId;
    }
}

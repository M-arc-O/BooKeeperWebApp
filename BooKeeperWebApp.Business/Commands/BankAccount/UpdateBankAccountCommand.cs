using BooKeeperWebApp.Business.CQRS;
using BooKeeperWebApp.Shared.Enums;

namespace BooKeeperWebApp.Business.Commands.BankAccount;
public class UpdateBankAccountCommand : ICommand
{
    public Guid UserId { get; }
    public Guid AccountId { get; }
    public string Name { get; }
    public string Number { get; }
    public AccountType Type { get; }
    public double StartAmount { get; }

    public UpdateBankAccountCommand(Guid userId, Guid accountId, string name, string number, AccountType type, double startAmount)
    {
        UserId = userId;
        AccountId = accountId;
        Name = name;
        Number = number;
        Type = type;
        StartAmount = startAmount;
    }
}

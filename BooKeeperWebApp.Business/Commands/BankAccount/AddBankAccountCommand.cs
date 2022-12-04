using BooKeeperWebApp.Business.CQRS;
using BooKeeperWebApp.Shared.Enums;

namespace BooKeeperWebApp.Business.Commands.BankAccount;
public class AddBankAccountCommand : ICommand
{
    public Guid UserId { get; }
    public string Name { get; }
    public string Number { get; }
    public AccountType Type { get; }
    public double StartAmount { get; }

    public AddBankAccountCommand(Guid userId, string name, string number, AccountType type, double startAmount)
    {
        UserId = userId;
        Name = name;
        Number = number;
        Type = type;
        StartAmount = startAmount;
    }
}

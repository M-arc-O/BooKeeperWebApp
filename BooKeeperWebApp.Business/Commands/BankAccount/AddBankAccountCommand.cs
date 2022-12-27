using BooKeeperWebApp.Business.CQRS;
using BooKeeperWebApp.Shared.Enums;

namespace BooKeeperWebApp.Business.Commands.BankAccount;
public class AddBankAccountCommand : ICommand
{
    public Guid UserId { get; }
    public string Name { get; }
    public string Number { get; }
    public BankAccountType Type { get; }
    public double StartAmount { get; }

    public AddBankAccountCommand(Guid userId, string name, string number, BankAccountType type, double startAmount)
    {
        UserId = userId;
        Name = name;
        Number = number;
        Type = type;
        StartAmount = startAmount;
    }
}

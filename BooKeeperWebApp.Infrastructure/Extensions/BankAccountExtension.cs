using BooKeeperWebApp.Infrastructure.Entities;

namespace BooKeeperWebApp.Infrastructure.Extensions;
public static class BankAccountExtension
{
    public static Shared.BankAccount ToSharedBankAccount(this BankAccount from)
    {
        return new Shared.BankAccount(from.Id, from.Name);
    }
}

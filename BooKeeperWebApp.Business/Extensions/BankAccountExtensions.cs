using BooKeeperWebApp.Business.Models;
using BooKeeperWebApp.Infrastructure.Entities;

namespace BooKeeperWebApp.Business.Extensions;
public static class BankAccountExtensions
{
    public static BankAccountModel ToBankAccountModel(this BankAccount from)
    {
        return new BankAccountModel
        {
            Id = from.Id,
            Name = from.Name,
        };
    }
}

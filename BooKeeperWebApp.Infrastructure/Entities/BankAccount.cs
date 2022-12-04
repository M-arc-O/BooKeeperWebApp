using BooKeeperWebApp.Infrastructure.Enums;

namespace BooKeeperWebApp.Infrastructure.Entities;
public class BankAccount
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string? Name { get; set; }
    public string? Number { get; set; }
    public AccountType Type { get; set; }
    public double StartAmount { get; set; }
    public double CurrentAmount { get; set; }
}

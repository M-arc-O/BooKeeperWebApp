using BooKeeperWebApp.Shared.Enums;

namespace BooKeeperWebApp.Business.Models;
public class BankAccountModel
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string? Name { get; set; }
    public string? Number { get; set; }
    public AccountType Type { get; set; }
    public double StartAmount { get; set; }
    public double CurrentAmount { get; set; }
}

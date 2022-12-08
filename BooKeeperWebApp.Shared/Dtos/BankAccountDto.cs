using BooKeeperWebApp.Shared.Enums;

namespace BooKeeperWebApp.Shared.Dtos;
public class BankAccountDto : IBaseDto
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Number { get; set; }
    public AccountType Type { get; set; }
    public double StartAmount { get; set; }
    public double CurrentAmount { get; set; }
}
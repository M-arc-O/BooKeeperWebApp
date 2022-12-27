using BooKeeperWebApp.Shared.Enums;

namespace BooKeeperWebApp.Shared.Dtos.Bank;
public class BankAccountDto : IBaseDto
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Number { get; set; }
    public BankAccountType Type { get; set; }
    public double StartAmount { get; set; }
    public double CurrentAmount { get; set; }
    public ICollection<MutationDto>? Mutations { get; set; }
}
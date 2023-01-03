using BooKeeperWebApp.Infrastructure.Enums;

namespace BooKeeperWebApp.Infrastructure.Entities.Bank;
public class BankAccount
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string? Name { get; set; }
    public string? Number { get; set; }
    public BankAccountType Type { get; set; }
    public double StartAmount { get; set; }
    public double CurrentAmount { get; set; }
    public ICollection<Mutation>? Mutations { get; set; }
    public ICollection<MonthlyValue>? MonthlyValues { get; set; }
    public ICollection<YearlyValue>? YearlyValues { get; set; }
}

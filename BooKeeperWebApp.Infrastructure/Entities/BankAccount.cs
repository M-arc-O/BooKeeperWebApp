namespace BooKeeperWebApp.Infrastructure.Entities;
public class BankAccount
{
    public Guid Id { get; internal set; }
    public string Name { get; internal set; }

    private BankAccount() { }

    public BankAccount(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
}

namespace BooKeeperWebApp.Infrastructure.Entities.Bank;
public class Mutation
{
    public Guid Id { get; set; }
    public DateTime Date { get; set; }
    public string AccountNumber { get; set; }
    public string OtherAccountNumber { get; set; }
    public string Description { get; set; }
    public string? Comment { get; set; }
    public string? Tag { get; set; }
    public double Amount { get; set; }
    public double AmountAfterMutation { get; set; }

    public BankAccount Account { get; set; }
    public Book Book { get; set; }
    public Event? Event { get; set; }

    public override bool Equals(object? obj)
    {
        if (obj is not Mutation)
        {
            return base.Equals(obj);
        }

        var other = obj as Mutation;

        return
            Date == other!.Date &&
            AccountNumber.Equals(other!.AccountNumber) &&
            OtherAccountNumber.Equals(other!.OtherAccountNumber) &&
            Description.Equals(other!.Description) &&
            Comment != null && Comment.Equals(other?.Comment) &&
            Tag != null && Tag.Equals(other?.Tag) &&
            Amount == other!.Amount &&
            AmountAfterMutation == other!.AmountAfterMutation;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}

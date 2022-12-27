namespace BooKeeperWebApp.Infrastructure.Entities.Bank;
public class Book
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string? Name { get; set; }
    public ICollection<Mutation>? Mutations { get; set; }
}

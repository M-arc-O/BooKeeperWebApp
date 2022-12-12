namespace BooKeeperWebApp.Business.Models;
public class BookModel
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string? Name { get; set; }
    public ICollection<MutationModel>? Mutations { get; set; }
}

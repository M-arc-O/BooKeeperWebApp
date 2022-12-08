namespace BooKeeperWebApp.Infrastructure.Entities;
public class Event
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string? Name { get; set; }
}

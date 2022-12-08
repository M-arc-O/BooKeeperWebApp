namespace BooKeeperWebApp.Business.Models;
public class UserModel
{
    public Guid Id { get; set; }
    public string? IdentityProvider { get; set; }
    public string? ProviderId { get; set; }
    public string? Details { get; set; }
}

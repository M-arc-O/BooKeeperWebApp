namespace BooKeeperWebApp.Shared;
public static class Roles
{
    public const string Admin = "admin";
    public const string User = "user";

    public static readonly string ApplicationUsers = $"{Admin}, {User}";
}

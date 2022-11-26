using BooKeeperWebApp.Business.Models;

namespace BooKeeperWebApp.Business.Services;
public interface IUserService
{
    Task MakeSureUserExists(UserModel user);
    Task<UserModel> GetUserByProviderId(string providerId);
}

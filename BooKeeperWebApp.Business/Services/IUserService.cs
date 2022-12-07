using BooKeeperWebApp.Business.Models;

namespace BooKeeperWebApp.Business.Services;
public interface IUserService
{
    Task<UserModel> GetUserByProviderIdAsync(string providerId);
}

using AutoMapper;
using BooKeeperWebApp.Business.Models;
using BooKeeperWebApp.Infrastructure.Contexts;
using BooKeeperWebApp.Infrastructure.Entities;
using BooKeeperWebApp.Infrastructure.Repositories;
using BooKeeperWebApp.Shared.Exceptions;

namespace BooKeeperWebApp.Business.Services;
public class UserService : IUserService
{
    private readonly IGenericRepository<User> _userRepository;
    private readonly BooKeeperWebAppDbContext _dbContext;
    private readonly IMapper _mapper;

	public UserService(IGenericRepository<User> userRepository, BooKeeperWebAppDbContext dbContext, IMapper mapper)
	{
		_userRepository = userRepository;
        _dbContext = dbContext;
        _mapper = mapper;
	}

    public async Task<UserModel> GetUserByProviderId(string providerId)
    {
        var user = await GetUser(providerId) ?? throw new NotFoundException($"User with provider id '{providerId}' not found.");
        return _mapper.Map<UserModel>(user);
    }

    public async Task MakeSureUserExists(UserModel userModel)
    {
        var user = await GetUser(userModel.ProviderId);

        if (user != null)
        {
            var newUser = _mapper.Map<User>(userModel);
            newUser.Id = Guid.NewGuid();
            await _userRepository.InsertAsync(newUser);
            await _dbContext.SaveChangesAsync();
        }
    }

    private async Task<User?> GetUser(string providerId)
    {
        var users = await _userRepository.GetAsync(x => x.ProviderId == providerId);
        return users?.FirstOrDefault();
    }
}

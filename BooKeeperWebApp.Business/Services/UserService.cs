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

    public async Task<UserModel> GetUserByProviderIdAsync(string providerId)
    {
        var user = await GetUserAsync(providerId) ?? throw new NotFoundException($"User with provider id '{providerId}' not found.");
        return _mapper.Map<UserModel>(user);
    }

    private async Task<User?> GetUserAsync(string providerId)
    {
        var users = await _userRepository.GetAsync(x => x.ProviderId == providerId);
        return users?.FirstOrDefault();
    }
}

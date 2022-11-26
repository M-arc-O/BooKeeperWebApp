using AutoMapper;
using BooKeeperWebApp.Business.Models;
using BooKeeperWebApp.Infrastructure.Entities;

namespace BooKeeperWebApp.Business.MappingProfiles;
public class UserModelProfiles : Profile
{
    public UserModelProfiles()
    {
        CreateMap<UserModel, User>();
        CreateMap<User, UserModel>();
    }
}

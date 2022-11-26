using AutoMapper;
using BooKeeperWebApp.Business.Models;
using BooKeeperWebApp.Shared.Models;

namespace Api.MappingProfiles;
public class UserProfiles : Profile
{
	public UserProfiles()
	{
		CreateMap<ClientPrincipal, UserModel>()
			.ForMember(dest => dest.ProviderId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.IdentityProvider, opt => opt.MapFrom(src => src.IdentityProvider))
            .ForMember(dest => dest.Details, opt => opt.MapFrom(src => src.UserDetails));
	}
}

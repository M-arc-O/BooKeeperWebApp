using AutoMapper;
using BooKeeperWebApp.Business.Models;
using BooKeeperWebApp.Shared.Dtos;

namespace Api.MappingProfiles;
public class MutationProfiles : Profile
{
	public MutationProfiles()
	{
		CreateMap<MutationModel, MutationDto>()
			.ForMember(dest => dest.BookName, opt => opt.MapFrom(src => src.Book.Name))
            .ForMember(dest => dest.EventName, opt => opt.MapFrom(src => src.Event != null ? src.Event.Name: string.Empty));
	}
}

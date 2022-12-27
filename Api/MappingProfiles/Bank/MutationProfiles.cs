using AutoMapper;
using BooKeeperWebApp.Business.Models.Bank;
using BooKeeperWebApp.Shared.Dtos.Bank;

namespace Api.MappingProfiles.Bank;
public class MutationProfiles : Profile
{
    public MutationProfiles()
    {
        CreateMap<MutationModel, MutationDto>()
            .ForMember(dest => dest.BookName, opt => opt.MapFrom(src => src.Book.Name))
            .ForMember(dest => dest.EventName, opt => opt.MapFrom(src => src.Event != null ? src.Event.Name : string.Empty));
    }
}

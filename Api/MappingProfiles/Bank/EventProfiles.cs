using AutoMapper;
using BooKeeperWebApp.Business.Models.Bank;
using BooKeeperWebApp.Shared.Dtos.Bank;

namespace Api.MappingProfiles.Bank;
public class EventProfiles : Profile
{
    public EventProfiles()
    {
        CreateMap<EventModel, EventDto>();
    }
}

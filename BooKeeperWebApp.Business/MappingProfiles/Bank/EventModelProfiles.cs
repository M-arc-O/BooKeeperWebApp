using AutoMapper;
using BooKeeperWebApp.Business.Models.Bank;
using BooKeeperWebApp.Infrastructure.Entities.Bank;

namespace BooKeeperWebApp.Business.MappingProfiles.Bank;
public class EventModelProfiles : Profile
{
    public EventModelProfiles()
    {
        CreateMap<EventModel, Event>();
        CreateMap<Event, EventModel>();
    }
}

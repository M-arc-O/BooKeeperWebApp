using AutoMapper;
using BooKeeperWebApp.Business.Models;
using BooKeeperWebApp.Infrastructure.Entities;

namespace BooKeeperWebApp.Business.MappingProfiles;
public class EventModelProfiles : Profile
{
    public EventModelProfiles()
    {
        CreateMap<EventModel, Event>();
        CreateMap<Event, EventModel>();
    }
}

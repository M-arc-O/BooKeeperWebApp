using AutoMapper;
using BooKeeperWebApp.Business.Models;
using BooKeeperWebApp.Shared.Dtos;

namespace Api.MappingProfiles;
public class EventProfiles : Profile
{
	public EventProfiles()
	{
		CreateMap<EventModel, EventDto>();
	}
}

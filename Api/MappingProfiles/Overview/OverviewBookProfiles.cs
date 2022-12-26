using AutoMapper;
using BooKeeperWebApp.Business.Models.Overview;
using BooKeeperWebApp.Shared.Dtos.Overview;

namespace Api.MappingProfiles.Overview;
public class OverviewBookProfiles : Profile
{
	public OverviewBookProfiles()
	{
		CreateMap<OverviewBookModel, OverviewBookDto>();
	}
}

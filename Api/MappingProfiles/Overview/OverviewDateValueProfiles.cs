using AutoMapper;
using BooKeeperWebApp.Business.Models.Overview;
using BooKeeperWebApp.Shared.Dtos.Overview;

namespace Api.MappingProfiles.Overview;
public class OverviewDateValueProfiles : Profile
{
	public OverviewDateValueProfiles()
	{
		CreateMap<OverviewDateValueModel, OverviewDateValueDto>();
	}
}

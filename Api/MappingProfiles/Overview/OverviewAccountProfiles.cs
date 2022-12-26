using AutoMapper;
using BooKeeperWebApp.Business.Models.Overview;
using BooKeeperWebApp.Shared.Dtos.Overview;

namespace Api.MappingProfiles.Overview;
public class OverviewAccountProfiles : Profile
{
	public OverviewAccountProfiles()
	{
		CreateMap<OverviewAccountModel, OverviewAccountDto>();
	}
}

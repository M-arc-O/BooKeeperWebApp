using AutoMapper;
using BooKeeperWebApp.Business.Models;
using BooKeeperWebApp.Shared.Dtos;

namespace Api.MappingProfiles;
public class MutationProfiles : Profile
{
	public MutationProfiles()
	{
		CreateMap<MutationModel, MutationDto>();
	}
}

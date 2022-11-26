using AutoMapper;
using BooKeeperWebApp.Business.Models;
using BooKeeperWebApp.Shared.Dtos;

namespace Api.MappingProfiles;
public class BankAccountProfiles : Profile
{
	public BankAccountProfiles()
	{
		CreateMap<BankAccountModel, BankAccountDto>();
	}
}

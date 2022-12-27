using AutoMapper;
using BooKeeperWebApp.Business.Models.Investment;
using BooKeeperWebApp.Shared.Dtos.Investment;

namespace Api.MappingProfiles.Investment;
public class InvestmentAccountProfiles : Profile
{
    public InvestmentAccountProfiles()
    {
        CreateMap<InvestmentAccountModel, InvestmentAccountDto>();
    }
}

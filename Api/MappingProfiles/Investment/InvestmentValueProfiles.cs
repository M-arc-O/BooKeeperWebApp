using AutoMapper;
using BooKeeperWebApp.Business.Models.Investment;
using BooKeeperWebApp.Shared.Dtos.Investment;

namespace Api.MappingProfiles.Investment;
public class InvestmentValueProfiles : Profile
{
    public InvestmentValueProfiles()
    {
        CreateMap<InvestmentValueModel, InvestmentValueDto>();
    }
}

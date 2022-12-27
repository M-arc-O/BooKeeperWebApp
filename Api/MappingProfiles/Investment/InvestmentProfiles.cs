using AutoMapper;
using BooKeeperWebApp.Business.Models.Investment;
using BooKeeperWebApp.Shared.Dtos.Investment;

namespace Api.MappingProfiles.Investment;
public class InvestmentProfiles : Profile
{
    public InvestmentProfiles()
    {
        CreateMap<InvestmentModel, InvestmentDto>();
    }
}

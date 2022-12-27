using AutoMapper;
using BooKeeperWebApp.Business.Models.Investment;
using BooKeeperWebApp.Infrastructure.Entities.Investment;

namespace BooKeeperWebApp.Business.MappingProfiles.Investment;
public class InvestmentValueModelProfiles : Profile
{
    public InvestmentValueModelProfiles()
    {
        CreateMap<InvestmentValueModel, InvestmentValue>();
        CreateMap<InvestmentValue, InvestmentValueModel>();
    }
}

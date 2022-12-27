using AutoMapper;
using BooKeeperWebApp.Business.Models.Investment;

namespace BooKeeperWebApp.Business.MappingProfiles.Investment;
public class InvestmentModelProfiles : Profile
{
    public InvestmentModelProfiles()
    {
        CreateMap<InvestmentModel, Infrastructure.Entities.Investment.Investment>();
        CreateMap<Infrastructure.Entities.Investment.Investment, InvestmentModel>();
    }
}

using AutoMapper;
using BooKeeperWebApp.Business.Models.Investment;
using BooKeeperWebApp.Infrastructure.Entities.Investment;

namespace BooKeeperWebApp.Business.MappingProfiles.Investment;
public class InvestmentAccountModelProfiles : Profile
{
    public InvestmentAccountModelProfiles()
    {
        CreateMap<InvestmentAccountModel, InvestmentAccount>();
        CreateMap<InvestmentAccount, InvestmentAccountModel>();
    }
}

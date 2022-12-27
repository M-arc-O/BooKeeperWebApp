using AutoMapper;
using BooKeeperWebApp.Business.Models.Bank;
using BooKeeperWebApp.Shared.Dtos.Bank;

namespace Api.MappingProfiles.Bank;
public class BankAccountProfiles : Profile
{
    public BankAccountProfiles()
    {
        CreateMap<BankAccountModel, BankAccountDto>();
    }
}

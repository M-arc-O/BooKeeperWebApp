using AutoMapper;
using BooKeeperWebApp.Business.Models.Bank;
using BooKeeperWebApp.Infrastructure.Entities.Bank;

namespace BooKeeperWebApp.Business.MappingProfiles.Bank;
public class BankAccountModelProfiles : Profile
{
    public BankAccountModelProfiles()
    {
        CreateMap<BankAccountModel, BankAccount>();
        CreateMap<BankAccount, BankAccountModel>();
    }
}

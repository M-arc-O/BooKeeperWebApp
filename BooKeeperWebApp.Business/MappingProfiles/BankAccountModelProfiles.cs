using AutoMapper;
using BooKeeperWebApp.Business.Models;
using BooKeeperWebApp.Infrastructure.Entities.Bank;

namespace BooKeeperWebApp.Business.MappingProfiles;
public class BankAccountModelProfiles : Profile
{
    public BankAccountModelProfiles()
    {
        CreateMap<BankAccountModel, BankAccount>();
        CreateMap<BankAccount, BankAccountModel>();
    }
}

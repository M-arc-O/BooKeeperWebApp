using AutoMapper;
using BooKeeperWebApp.Business.Models.Bank;
using BooKeeperWebApp.Infrastructure.Entities.Bank;

namespace BooKeeperWebApp.Business.MappingProfiles.Bank;
public class MutationModelProfiles : Profile
{
    public MutationModelProfiles()
    {
        CreateMap<MutationModel, Mutation>();
        CreateMap<Mutation, MutationModel>();
    }
}

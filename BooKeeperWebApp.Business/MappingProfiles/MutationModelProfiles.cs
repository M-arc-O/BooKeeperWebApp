using AutoMapper;
using BooKeeperWebApp.Business.Models;
using BooKeeperWebApp.Infrastructure.Entities;

namespace BooKeeperWebApp.Business.MappingProfiles;
public class MutationModelProfiles : Profile
{
    public MutationModelProfiles()
    {
        CreateMap<MutationModel, Mutation>();
        CreateMap<Mutation, MutationModel>();
    }
}

using AutoMapper;
using BooKeeperWebApp.Shared.Models;

namespace BooKeeperWebApp.Shared.Services.Csv.CsvModels;
public class RegioBankCsvModelMappingProfile : Profile
{
	public RegioBankCsvModelMappingProfile()
	{
        CreateMap<RegiobankCsvModel, AddMutationModel>()
            .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
            .ForMember(dest => dest.AccountNumber, opt => opt.MapFrom(src => src.Account))
            .ForMember(dest => dest.OtherAccountNumber, opt => opt.MapFrom(src => src.OtherAccount))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Comment, opt => opt.MapFrom(src => src.Comment))
            .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Amount))
            .ForMember(dest => dest.AmountAfterMutation, opt => opt.MapFrom(src => src.AmountBeforeMutation + src.Amount));
    }
}

using AutoMapper;
using BooKeeperWebApp.Shared.Models;

namespace BooKeeperWebApp.Shared.Services.Csv.CsvModels;
public class IngSavingCsvModelMappingProfile : Profile
{
	public IngSavingCsvModelMappingProfile()
	{
        CreateMap<IngSavingCsvModel, AddMutationModel>()
            .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
            .ForMember(dest => dest.AccountNumber, opt => opt.MapFrom(src => src.Account))
            .ForMember(dest => dest.OtherAccountNumber, opt => opt.MapFrom(src => src.OtherAccount))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Comment, opt => opt.MapFrom(src => src.Comment))
            .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Direction.Equals("Af") ? -1 * src.Amount : src.Amount))
            .ForMember(dest => dest.AmountAfterMutation, opt => opt.MapFrom(src => src.AmountAfterMutation));
    }
}

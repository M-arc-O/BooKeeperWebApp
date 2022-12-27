using AutoMapper;
using BooKeeperWebApp.Shared.Models.Bank;

namespace BooKeeperWebApp.Shared.Services.Csv.CsvModels;
public class IngPaymentCsvModelMappingProfile : Profile
{
	public IngPaymentCsvModelMappingProfile()
	{
        CreateMap<IngPaymentCsvModel, AddMutationModel>()
            .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
            .ForMember(dest => dest.AccountNumber, opt => opt.MapFrom(src => src.Account))
            .ForMember(dest => dest.OtherAccountNumber, opt => opt.MapFrom(src => src.OtherAccount))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Comment, opt => opt.MapFrom(src => src.Comment))
            .ForMember(dest => dest.Tag, opt => opt.MapFrom(src => src.Tag))
            .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Direction.Equals("Af") ? -1 * src.Amount: src.Amount))
            .ForMember(dest => dest.AmountAfterMutation, opt => opt.MapFrom(src => src.AmountAfterMutation));
    }
}

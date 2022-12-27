using AutoMapper;
using BooKeeperWebApp.Business.Models.Bank;
using BooKeeperWebApp.Shared.Dtos.Bank;

namespace Api.MappingProfiles.Bank;
public class BookProfiles : Profile
{
    public BookProfiles()
    {
        CreateMap<BookModel, BookDto>();
    }
}

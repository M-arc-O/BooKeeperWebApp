using AutoMapper;
using BooKeeperWebApp.Business.Models.Bank;
using BooKeeperWebApp.Infrastructure.Entities.Bank;

namespace BooKeeperWebApp.Business.MappingProfiles.Bank;
public class BookModelProfiles : Profile
{
    public BookModelProfiles()
    {
        CreateMap<BookModel, Book>();
        CreateMap<Book, BookModel>();
    }
}

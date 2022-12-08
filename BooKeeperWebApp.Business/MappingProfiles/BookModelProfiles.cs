using AutoMapper;
using BooKeeperWebApp.Business.Models;
using BooKeeperWebApp.Infrastructure.Entities;

namespace BooKeeperWebApp.Business.MappingProfiles;
public class BookModelProfiles : Profile
{
    public BookModelProfiles()
    {
        CreateMap<BookModel, Book>();
        CreateMap<Book, BookModel>();
    }
}

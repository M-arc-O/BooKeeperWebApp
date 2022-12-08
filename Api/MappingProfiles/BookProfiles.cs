using AutoMapper;
using BooKeeperWebApp.Business.Models;
using BooKeeperWebApp.Shared.Dtos;

namespace Api.MappingProfiles;
public class BookProfiles : Profile
{
	public BookProfiles()
	{
		CreateMap<BookModel, BookDto>();
	}
}

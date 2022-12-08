using AutoMapper;
using BooKeeperWebApp.Business.CQRS;
using BooKeeperWebApp.Business.Models;
using BooKeeperWebApp.Infrastructure.Repositories;

namespace BooKeeperWebApp.Business.Queries.Book;
public class GetAllBooksQueryHandler : IHandler<GetAllBooksQuery, IEnumerable<BookModel>>
{
    private readonly IGenericRepository<Infrastructure.Entities.Book> _bookRepository;
    private readonly IMapper _mapper;

    public GetAllBooksQueryHandler(IGenericRepository<Infrastructure.Entities.Book> bookRepository, IMapper mapper)
    {
        _bookRepository = bookRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<BookModel>> ExecuteAsync(GetAllBooksQuery query)
    {
        var books = await _bookRepository.GetAsync(x => x.UserId == query.UserId);
        return books.Select(x => _mapper.Map<BookModel>(x));
    }
}

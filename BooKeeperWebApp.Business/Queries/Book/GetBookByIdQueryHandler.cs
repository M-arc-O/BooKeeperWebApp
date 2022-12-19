using AutoMapper;
using BooKeeperWebApp.Business.CQRS;
using BooKeeperWebApp.Business.Models;
using BooKeeperWebApp.Infrastructure.Repositories;
using BooKeeperWebApp.Shared.Exceptions;

namespace BooKeeperWebApp.Business.Queries.Book;
public class GetBookByIdQueryHandler : IHandler<GetBookByIdQuery, BookModel>
{
    private readonly IGenericRepository<Infrastructure.Entities.Book> _bookRepository;
    private readonly IMapper _mapper;

    public GetBookByIdQueryHandler(IGenericRepository<Infrastructure.Entities.Book> bookRepository, IMapper mapper)
    {
        _bookRepository = bookRepository;
        _mapper = mapper;
    }

    public async Task<BookModel> ExecuteAsync(GetBookByIdQuery query)
    {
        var books = await _bookRepository.GetAsync(x => x.UserId == query.UserId, null, "Mutations");
        var book = books.FirstOrDefault(x => x.Id == query.BookId)
            ?? throw new NotFoundException($"Book with id '{query.BookId}' could not be found.");

        return _mapper.Map<BookModel>(book);
    }
}

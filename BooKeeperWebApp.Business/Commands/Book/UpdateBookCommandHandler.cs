using AutoMapper;
using BooKeeperWebApp.Business.CQRS;
using BooKeeperWebApp.Business.Models.Bank;
using BooKeeperWebApp.Infrastructure.Repositories;
using BooKeeperWebApp.Shared.Exceptions;

namespace BooKeeperWebApp.Business.Commands.Book;
public class UpdateBookCommandHandler : BookCommandBase, IHandler<UpdateBookCommand, BookModel>
{
    private readonly IMapper _mapper;

    public UpdateBookCommandHandler(IGenericRepository<Infrastructure.Entities.Bank.Book> bookRepository, IMapper mapper)
        : base(bookRepository)
    {
        _mapper = mapper;
    }

    public async Task<BookModel> ExecuteAsync(UpdateBookCommand command)
    {
        var book = await GetBookAsync(command.UserId, command.BookId);

        ValidateName(command.Name);

        if (command.Name != book.Name && await NameTakenAsync(command.Name))
        {
            throw new ValidationException($"Account with number '{command.Name}' already exists");
        }

        book.Name = command.Name;
        _bookRepository.Update(book);

        return _mapper.Map<BookModel>(book);
    }
}

﻿using AutoMapper;
using BooKeeperWebApp.Business.CQRS;
using BooKeeperWebApp.Business.Models.Bank;
using BooKeeperWebApp.Infrastructure.Repositories;
using BooKeeperWebApp.Shared.Exceptions;

namespace BooKeeperWebApp.Business.Commands.Book;
public class AddBookCommandHandler : BookCommandBase, IHandler<AddBookCommand, BookModel>
{
    private readonly IMapper _mapper;

    public AddBookCommandHandler(IGenericRepository<Infrastructure.Entities.Bank.Book> bookRepository, IMapper mapper)
        : base(bookRepository)
    {
        _mapper = mapper;
    }

    public async Task<BookModel> ExecuteAsync(AddBookCommand command)
    {
        var book = new Infrastructure.Entities.Bank.Book
        {
            Id = Guid.NewGuid(),
            UserId = command.UserId,
            Name = command.Name
        };

        ValidateName(command.Name);

        if (await NameTakenAsync(command.UserId, command.Name))
        {
            throw new ValidationException($"Book with name '{command.Name}' already exists");
        }

        await _bookRepository.InsertAsync(book);

        return _mapper.Map<BookModel>(book);
    }
}

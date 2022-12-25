﻿using BooKeeperWebApp.Shared.Dtos;
using BooKeeperWebApp.Shared.Models;
using Radzen;

namespace Client.Services;

public class BookService : HttpServiceBase<BookDto, AddBookModel>
{
    public BookService(HttpClient httpClient, NotificationService notificationService) 
        : base(httpClient, notificationService, "/api/book/")
    {
    }

    public async Task<bool> CreateBookAsync(BookDto book)
    {
        var newBook = new AddBookModel(book.Name!);
        return await CreateAsync(newBook);
    }

    public async Task<bool> UpdateBookAsync(BookDto book)
    {
        var updateBook = new AddBookModel(book.Name!);
        return await UpdateAsync(updateBook, book.Id);
    }
}
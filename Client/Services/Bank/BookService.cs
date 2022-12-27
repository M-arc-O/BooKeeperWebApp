using BooKeeperWebApp.Shared.Dtos.Bank;
using BooKeeperWebApp.Shared.Models.Bank;
using Radzen;

namespace Client.Services.Bank;

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

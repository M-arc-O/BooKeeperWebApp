using BooKeeperWebApp.Business.Commands.BankAccount;
using BooKeeperWebApp.Business.Commands.Book;
using BooKeeperWebApp.Business.Commands.Event;
using BooKeeperWebApp.Business.CQRS;
using BooKeeperWebApp.Business.Models;
using BooKeeperWebApp.Business.Queries.BankAccount;
using BooKeeperWebApp.Business.Queries.Book;
using BooKeeperWebApp.Business.Queries.Event;
using BooKeeperWebApp.Business.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BooKeeperWebApp.Business.Configuration;
public static class DependencyInjectionConfiguration
{
    public static void AddBusinessServices(this IServiceCollection services)
    {
        services.AddScoped<IExecutor, Executor>();
        services.AddScoped<IUserService, UserService>();

        services.AddScoped<IHandler<GetAllAccountsQuery, IEnumerable<BankAccountModel>>, GetAllAccountsQueryHandler>();
        services.AddScoped<IHandler<AddBankAccountCommand, BankAccountModel>, AddBankAccountCommandHandler>();
        services.AddScoped<IHandler<UpdateBankAccountCommand, BankAccountModel>, UpdateBankAccountCommandHandler>();
        services.AddScoped<IHandler<DeleteBankAccountCommand, Guid>, DeleteBankAccountCommandHandler>();

        services.AddScoped<IHandler<GetAllBooksQuery, IEnumerable<BookModel>>, GetAllBooksQueryHandler>();
        services.AddScoped<IHandler<AddBookCommand, BookModel>, AddBookCommandHandler>();
        services.AddScoped<IHandler<UpdateBookCommand, BookModel>, UpdateBookCommandHandler>();
        services.AddScoped<IHandler<DeleteBookCommand, Guid>, DeleteBookCommandHandler>();

        services.AddScoped<IHandler<GetAllEventsQuery, IEnumerable<EventModel>>, GetAllEventsQueryHandler>();
        services.AddScoped<IHandler<AddEventCommand, EventModel>, AddEventCommandHandler>();
        services.AddScoped<IHandler<UpdateEventCommand, EventModel>, UpdateEventCommandHandler>();
        services.AddScoped<IHandler<DeleteEventCommand, Guid>, DeleteEventCommandHandler>();
    }
}

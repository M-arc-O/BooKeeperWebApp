using BooKeeperWebApp.Business.Commands.BankAccount;
using BooKeeperWebApp.Business.Commands.Book;
using BooKeeperWebApp.Business.Commands.Event;
using BooKeeperWebApp.Business.Commands.Mutation;
using BooKeeperWebApp.Business.CQRS;
using BooKeeperWebApp.Business.Models;
using BooKeeperWebApp.Business.Models.Overview;
using BooKeeperWebApp.Business.Queries.BankAccount;
using BooKeeperWebApp.Business.Queries.Book;
using BooKeeperWebApp.Business.Queries.Event;
using BooKeeperWebApp.Business.Queries.Mutation;
using BooKeeperWebApp.Business.Queries.Overview;
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
        services.AddScoped<IHandler<GetAccountByIdQuery, BankAccountModel>, GetAccountByIdQueryHandler>();
        services.AddScoped<IHandler<AddBankAccountCommand, BankAccountModel>, AddBankAccountCommandHandler>();
        services.AddScoped<IHandler<UpdateBankAccountCommand, BankAccountModel>, UpdateBankAccountCommandHandler>();
        services.AddScoped<IHandler<DeleteBankAccountCommand, Guid>, DeleteBankAccountCommandHandler>();

        services.AddScoped<IHandler<GetAllBooksQuery, IEnumerable<BookModel>>, GetAllBooksQueryHandler>();
        services.AddScoped<IHandler<GetBookByIdQuery, BookModel>, GetBookByIdQueryHandler>();
        services.AddScoped<IHandler<AddBookCommand, BookModel>, AddBookCommandHandler>();
        services.AddScoped<IHandler<UpdateBookCommand, BookModel>, UpdateBookCommandHandler>();
        services.AddScoped<IHandler<DeleteBookCommand, Guid>, DeleteBookCommandHandler>();

        services.AddScoped<IHandler<GetAllEventsQuery, IEnumerable<EventModel>>, GetAllEventsQueryHandler>();
        services.AddScoped<IHandler<GetEventByIdQuery, EventModel>, GetEventByIdQueryHandler>();
        services.AddScoped<IHandler<AddEventCommand, EventModel>, AddEventCommandHandler>();
        services.AddScoped<IHandler<UpdateEventCommand, EventModel>, UpdateEventCommandHandler>();
        services.AddScoped<IHandler<DeleteEventCommand, Guid>, DeleteEventCommandHandler>();

        services.AddScoped<IHandler<GetAllMutationsByAccountIdQuery, IEnumerable<MutationModel>>, GetAllMutationsByAccountIdQueryHandler>();
        services.AddScoped<IHandler<GetAllMutationsByBookIdQuery, IEnumerable<MutationModel>>, GetAllMutationsByBookIdQueryHandler>();
        services.AddScoped<IHandler<GetAllMutationsByEventIdQuery, IEnumerable<MutationModel>>, GetAllMutationsByEventIdQueryHandler>();
        services.AddScoped<IHandler<AddMutationCommand, MutationModel>, AddMutationCommandHandler>();
        services.AddScoped<IHandler<AddMultipleMutationsCommand, MutationModel[]>, AddMultipleMutationsCommandHandler>();
        services.AddScoped<IHandler<UpdateMutationCommand, MutationModel>, UpdateMutationCommandHandler>();
        services.AddScoped<IHandler<DeleteMutationCommand, Guid>, DeleteMutationCommandHandler>();

        services.AddScoped<IHandler<GetBooksOverviewQuery, IEnumerable<OverviewBookModel>>, GetBooksOverviewQueryHandler>();
    }
}

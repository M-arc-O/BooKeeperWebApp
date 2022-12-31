using BooKeeperWebApp.Business.Commands.BankAccount;
using BooKeeperWebApp.Business.Commands.Book;
using BooKeeperWebApp.Business.Commands.Event;
using BooKeeperWebApp.Business.Commands.Investment;
using BooKeeperWebApp.Business.Commands.InvestmentAccount;
using BooKeeperWebApp.Business.Commands.InvestmentValue;
using BooKeeperWebApp.Business.Commands.Mutation;
using BooKeeperWebApp.Business.CQRS;
using BooKeeperWebApp.Business.Models.Bank;
using BooKeeperWebApp.Business.Models.Investment;
using BooKeeperWebApp.Business.Models.Overview;
using BooKeeperWebApp.Business.Queries.BankAccount;
using BooKeeperWebApp.Business.Queries.Book;
using BooKeeperWebApp.Business.Queries.Event;
using BooKeeperWebApp.Business.Queries.InvestmentAccount;
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

        services.AddScoped<IHandler<GetAllBankAccountsQuery, IEnumerable<BankAccountModel>>, GetAllBankAccountsQueryHandler>();
        services.AddScoped<IHandler<GetBankAccountByIdQuery, BankAccountModel>, GetBankAccountByIdQueryHandler>();
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

        services.AddScoped<IHandler<GetAllInvestmentAccountsQuery, IEnumerable<InvestmentAccountModel>>, GetAllInvestmentAccountsQueryHandler>();
        services.AddScoped<IHandler<GetInvestmentAccountByIdQuery, InvestmentAccountModel>, GetInvestmentAccountByIdQueryHandler>();
        services.AddScoped<IHandler<AddInvestmentAccountCommand, InvestmentAccountModel>, AddInvestmentAccountCommandHandler>();
        services.AddScoped<IHandler<UpdateInvestmentAccountCommand, InvestmentAccountModel>, UpdateInvestmentAccountCommandHandler>();
        services.AddScoped<IHandler<DeleteInvestmentAccountCommand, Guid>, DeleteInvestmentAccountCommandHandler>();

        services.AddScoped<IHandler<AddInvestmentCommand, InvestmentModel>, AddInvestmentCommandHandler>();
        services.AddScoped<IHandler<UpdateInvestmentCommand, InvestmentModel>, UpdateInvestmentCommandHandler>();
        services.AddScoped<IHandler<DeleteInvestmentCommand, Guid>, DeleteInvestmentCommandHandler>();

        services.AddScoped<IHandler<AddInvestmentValueCommand, InvestmentValueModel>, AddInvestmentValueCommandHandler>();
        services.AddScoped<IHandler<DeleteInvestmentValueCommand, Guid>, DeleteInvestmentValueCommandHandler>();

        services.AddScoped<IHandler<GetAccountsOverviewQuery, IEnumerable<OverviewAccountModel>>, GetAccountsOverviewQueryHandler>();
        services.AddScoped<IHandler<GetAccountChartOverviewQuery, IEnumerable<OverviewDateValueModel>>, GetAccountChartOverviewQueryHandler>();
        services.AddScoped<IHandler<GetBooksOverviewQuery, IEnumerable<OverviewBookModel>>, GetBooksOverviewQueryHandler>();
    }
}

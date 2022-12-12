using AutoMapper;
using BooKeeperWebApp.Business.CQRS;
using BooKeeperWebApp.Business.Models;
using BooKeeperWebApp.Infrastructure.Repositories;
using BooKeeperWebApp.Shared.Exceptions;

namespace BooKeeperWebApp.Business.Commands.Mutation;
public class UpdateMutationCommandHandler : MutationCommandBase, IHandler<UpdateMutationCommand, MutationModel>
{
    private readonly IMapper _mapper;

    public UpdateMutationCommandHandler(
        IGenericRepository<Infrastructure.Entities.BankAccount> accountRepository,
        IGenericRepository<Infrastructure.Entities.Book> bookRepository,
        IGenericRepository<Infrastructure.Entities.Event> eventRepository,
        IGenericRepository<Infrastructure.Entities.Mutation> mutationRepository,
        IMapper mapper)
        : base(accountRepository, bookRepository, eventRepository, mutationRepository)
    {
        _mapper = mapper;
    }

    public async Task<MutationModel> ExecuteAsync(UpdateMutationCommand command)
    {
        var entitie = await GetMutationAsync(command.UserId, command.MutationId);

        entitie.Account = await GetAccountAsync(command.UserId, command.AccountId);
        entitie.Book = await GetBookAsync(command.UserId, command.BookId);

        if (command.EventId.HasValue)
        {
            entitie.Event = await GetEventAsync(command.UserId, command.EventId.Value);
        }
        _mutationRepository.Update(entitie);

        return _mapper.Map<MutationModel>(entitie);
    }
}

using AutoMapper;
using BooKeeperWebApp.Business.CQRS;
using BooKeeperWebApp.Business.Models;
using BooKeeperWebApp.Infrastructure.Repositories;

namespace BooKeeperWebApp.Business.Commands.Mutation;
public class UpdateMutationCommandHandler : MutationCommandBase, IHandler<UpdateMutationCommand, MutationModel>
{
    private readonly IMapper _mapper;

    public UpdateMutationCommandHandler(
        IGenericRepository<Infrastructure.Entities.Bank.BankAccount> accountRepository,
        IGenericRepository<Infrastructure.Entities.Bank.Book> bookRepository,
        IGenericRepository<Infrastructure.Entities.Bank.Event> eventRepository,
        IGenericRepository<Infrastructure.Entities.Bank.Mutation> mutationRepository,
        IMapper mapper)
        : base(accountRepository, bookRepository, eventRepository, mutationRepository)
    {
        _mapper = mapper;
    }

    public async Task<MutationModel> ExecuteAsync(UpdateMutationCommand command)
    {
        var entitie = await GetMutationAsync(command.UserId, command.MutationId);

        entitie.Book = await GetBookAsync(command.UserId, command.BookId);

        if (command.EventId.HasValue)
        {
            entitie.Event = await GetEventAsync(command.UserId, command.EventId.Value);
        }
        _mutationRepository.Update(entitie);

        return _mapper.Map<MutationModel>(entitie);
    }
}

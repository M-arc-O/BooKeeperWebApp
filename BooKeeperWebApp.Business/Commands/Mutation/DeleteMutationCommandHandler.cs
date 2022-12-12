using BooKeeperWebApp.Business.CQRS;
using BooKeeperWebApp.Infrastructure.Repositories;

namespace BooKeeperWebApp.Business.Commands.Mutation;
public class DeleteMutationCommandHandler : MutationCommandBase, IHandler<DeleteMutationCommand, Guid>
{
    public DeleteMutationCommandHandler(
        IGenericRepository<Infrastructure.Entities.BankAccount> accountRepository,
        IGenericRepository<Infrastructure.Entities.Book> bookRepository,
        IGenericRepository<Infrastructure.Entities.Event> eventRepository,
        IGenericRepository<Infrastructure.Entities.Mutation> mutationRepository)
        : base(accountRepository, bookRepository, eventRepository, mutationRepository)
    {
    }

    public async Task<Guid> ExecuteAsync(DeleteMutationCommand command)
    {
        var entitie = await GetMutationAsync(command.UserId, command.MutationId);
        _mutationRepository.Delete(entitie);

        return command.MutationId;
    }
}

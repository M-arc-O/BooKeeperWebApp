using BooKeeperWebApp.Business.CQRS;
using BooKeeperWebApp.Infrastructure.Repositories;

namespace BooKeeperWebApp.Business.Commands.Mutation;
public class DeleteMutationCommandHandler : MutationCommandBase, IHandler<DeleteMutationCommand, Guid>
{
    public DeleteMutationCommandHandler(
        IGenericRepository<Infrastructure.Entities.Bank.BankAccount> accountRepository,
        IGenericRepository<Infrastructure.Entities.Bank.Book> bookRepository,
        IGenericRepository<Infrastructure.Entities.Bank.Event> eventRepository,
        IGenericRepository<Infrastructure.Entities.Bank.Mutation> mutationRepository)
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

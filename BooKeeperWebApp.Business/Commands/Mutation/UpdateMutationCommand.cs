using BooKeeperWebApp.Business.CQRS;

namespace BooKeeperWebApp.Business.Commands.Mutation;
public record UpdateMutationCommand(
        Guid UserId,
        Guid MutationId,
        Guid AccountId,
        Guid BookId,
        Guid? EventId) : ICommand;

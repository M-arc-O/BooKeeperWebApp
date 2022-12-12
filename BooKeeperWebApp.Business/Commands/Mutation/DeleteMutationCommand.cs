using BooKeeperWebApp.Business.CQRS;

namespace BooKeeperWebApp.Business.Commands.Mutation;
public class DeleteMutationCommand : ICommand
{
    public Guid UserId { get; }
    public Guid MutationId { get; }

    public DeleteMutationCommand(Guid userId, Guid mutationId)
    {
        UserId = userId;
        MutationId = mutationId;
    }
}

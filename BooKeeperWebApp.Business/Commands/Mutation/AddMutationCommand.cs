using BooKeeperWebApp.Business.CQRS;

namespace BooKeeperWebApp.Business.Commands.Mutation;
public record AddMutationCommand(
        Guid UserId,
        DateTime Date,
        string AccountNumber,
        string OtherAccountNumber,
        string Description,
        string? Comment,
        string? Tag,
        double Amount,
        double AmountAfterMutation,
        Guid AccountId,
        Guid BookId,
        Guid? EventId) : ICommand;

using BooKeeperWebApp.Business.CQRS;

namespace BooKeeperWebApp.Business.Commands.Mutation;
public record AddMultipleMutationsCommand(AddMutationCommand[] mutations) : ICommand;

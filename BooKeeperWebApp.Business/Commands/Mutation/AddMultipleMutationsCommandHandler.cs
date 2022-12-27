using AutoMapper;
using BooKeeperWebApp.Business.CQRS;
using BooKeeperWebApp.Business.Models.Bank;
using BooKeeperWebApp.Infrastructure.Repositories;

namespace BooKeeperWebApp.Business.Commands.Mutation;
public class AddMultipleMutationsCommandHandler : MutationCommandBase, IHandler<AddMultipleMutationsCommand, MutationModel[]>
{
    private readonly IMapper _mapper;

    public AddMultipleMutationsCommandHandler(
        IGenericRepository<Infrastructure.Entities.Bank.BankAccount> accountRepository,
        IGenericRepository<Infrastructure.Entities.Bank.Book> bookRepository,
        IGenericRepository<Infrastructure.Entities.Bank.Event> eventRepository,
        IGenericRepository<Infrastructure.Entities.Bank.Mutation> mutationRepository,
        IMapper mapper)
        : base(accountRepository, bookRepository, eventRepository, mutationRepository)
    {
        _mapper = mapper;
    }

    public async Task<MutationModel[]> ExecuteAsync(AddMultipleMutationsCommand command)
    {
        var retVal = new List<MutationModel>();

        foreach (var mutation in command.mutations)
        {
            var newMutation = await CreateMutation(mutation);
            retVal.Add(_mapper.Map<MutationModel>(newMutation));
        }

        return retVal.ToArray();
    }
}

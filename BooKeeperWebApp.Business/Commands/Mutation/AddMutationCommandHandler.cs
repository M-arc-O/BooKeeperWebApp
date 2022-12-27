using AutoMapper;
using BooKeeperWebApp.Business.CQRS;
using BooKeeperWebApp.Business.Models;
using BooKeeperWebApp.Infrastructure.Repositories;

namespace BooKeeperWebApp.Business.Commands.Mutation;
public class AddMutationCommandHandler : MutationCommandBase, IHandler<AddMutationCommand, MutationModel>
{
    private readonly IMapper _mapper;

    public AddMutationCommandHandler(
        IGenericRepository<Infrastructure.Entities.Bank.BankAccount> accountRepository,
        IGenericRepository<Infrastructure.Entities.Bank.Book> bookRepository,
        IGenericRepository<Infrastructure.Entities.Bank.Event> eventRepository,
        IGenericRepository<Infrastructure.Entities.Bank.Mutation> mutationRepository,
        IMapper mapper)
        : base(accountRepository, bookRepository, eventRepository, mutationRepository)
    {
        _mapper = mapper;
    }

    public async Task<MutationModel> ExecuteAsync(AddMutationCommand command)
    {
        var mutation = await CreateMutation(command);
        return _mapper.Map<MutationModel>(mutation);
    }
}

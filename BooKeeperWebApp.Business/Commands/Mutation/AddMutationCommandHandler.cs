using AutoMapper;
using BooKeeperWebApp.Business.CQRS;
using BooKeeperWebApp.Business.Models;
using BooKeeperWebApp.Infrastructure.Repositories;
using BooKeeperWebApp.Shared.Exceptions;

namespace BooKeeperWebApp.Business.Commands.Mutation;
public class AddMutationCommandHandler : MutationCommandBase, IHandler<AddMutationCommand, MutationModel>
{
    private readonly IMapper _mapper;

    public AddMutationCommandHandler(
        IGenericRepository<Infrastructure.Entities.BankAccount> accountRepository,
        IGenericRepository<Infrastructure.Entities.Book> bookRepository,
        IGenericRepository<Infrastructure.Entities.Event> eventRepository,
        IGenericRepository<Infrastructure.Entities.Mutation> mutationRepository,
        IMapper mapper)
        : base(accountRepository, bookRepository, eventRepository, mutationRepository)
    {
        _mapper = mapper;
    }

    public async Task<MutationModel> ExecuteAsync(AddMutationCommand command)
    {
        var entitie = new Infrastructure.Entities.Mutation
        {
            Id = Guid.NewGuid(),
            Date = command.Date,
            AccountNumber = command.AccountNumber,
            OtherAccountNumber = command.OtherAccountNumber,
            Description = command.Description,
            Comment = command.Comment,
            Tag = command.Tag,
            Amount = command.Amount,
            AmountAfterMutation = command.AmountAfterMutation,
            Account = await GetAccountAsync(command.UserId, command.AccountId),
            Book = await GetBookAsync(command.UserId, command.BookId),
        };

        if (command.EventId.HasValue)
        {
            entitie.Event = await GetEventAsync(command.UserId, command.EventId.Value);
        }

        if (await MutionExistsAsync(entitie))
        {
            throw new ValidationException($"An mutation with these values already exists");
        }

        await _mutationRepository.InsertAsync(entitie);

        return _mapper.Map<MutationModel>(entitie);
    }
}

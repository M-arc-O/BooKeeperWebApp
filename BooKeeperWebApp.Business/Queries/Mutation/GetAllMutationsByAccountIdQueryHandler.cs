using AutoMapper;
using BooKeeperWebApp.Business.CQRS;
using BooKeeperWebApp.Business.Models;
using BooKeeperWebApp.Infrastructure.Repositories;
using BooKeeperWebApp.Shared.Exceptions;

namespace BooKeeperWebApp.Business.Queries.Mutation;
public class GetAllMutationsByAccountIdQueryHandler : IHandler<GetAllMutationsByAccountIdQuery, IEnumerable<MutationModel>>
{
    protected readonly IGenericRepository<Infrastructure.Entities.Mutation> _mutationRepository;
    protected readonly IGenericRepository<Infrastructure.Entities.BankAccount> _bankAccountRepository;
    private readonly IMapper _mapper;

    public GetAllMutationsByAccountIdQueryHandler(
        IGenericRepository<Infrastructure.Entities.Mutation> mutationRepository,
        IGenericRepository<Infrastructure.Entities.BankAccount> bankAccountRepository,
        IMapper mapper)
    {
        _mutationRepository = mutationRepository;
        _bankAccountRepository = bankAccountRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<MutationModel>> ExecuteAsync(GetAllMutationsByAccountIdQuery query)
    {
        CheckIfUserHasAccessToAccount(query.UserId, query.AccountId);

        var mutations = await _mutationRepository.GetAsync(x => x.Account.Id == query.AccountId);
        return mutations.Select(x => _mapper.Map<MutationModel>(x));
    }

    private async void CheckIfUserHasAccessToAccount(Guid userId, Guid accountId)
    {
        var bankAccount = await _bankAccountRepository.GetByIdAsync(accountId) ?? throw new NotFoundException($"Bank account with id '{accountId}' not found.");

        if (bankAccount.UserId != userId)
        {
            throw new UnauthorizedAccessException($"User with id '{userId}' does not have access to bank account with id '{accountId}'.");
        }
    }
}

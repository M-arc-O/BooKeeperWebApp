using AutoMapper;
using BooKeeperWebApp.Business.CQRS;
using BooKeeperWebApp.Business.Models;
using BooKeeperWebApp.Infrastructure.Repositories;
using BooKeeperWebApp.Shared.Exceptions;

namespace BooKeeperWebApp.Business.Queries.Mutation;
public class GetAllMutationsByBookIdQueryHandler : IHandler<GetAllMutationsByBookIdQuery, IEnumerable<MutationModel>>
{
    protected readonly IGenericRepository<Infrastructure.Entities.Bank.Mutation> _mutationRepository;
    protected readonly IGenericRepository<Infrastructure.Entities.Bank.Book> _bookRepository;
    private readonly IMapper _mapper;

    public GetAllMutationsByBookIdQueryHandler(
        IGenericRepository<Infrastructure.Entities.Bank.Mutation> mutationRepository,
        IGenericRepository<Infrastructure.Entities.Bank.Book> bookRepository,
        IMapper mapper)
    {
        _mutationRepository = mutationRepository;
        _bookRepository = bookRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<MutationModel>> ExecuteAsync(GetAllMutationsByBookIdQuery query)
    {
        CheckIfUserHasAccessToBook(query.UserId, query.BookId);

        var mutations = await _mutationRepository.GetAsync(x => x.Book.Id == query.BookId);
        return mutations.Select(x => _mapper.Map<MutationModel>(x));
    }

    private async void CheckIfUserHasAccessToBook(Guid userId, Guid bookId)
    {
        var bankAccount = await _bookRepository.GetByIdAsync(bookId) ?? throw new NotFoundException($"Book with id '{bookId}' not found.");

        if (bankAccount.UserId != userId)
        {
            throw new UnauthorizedAccessException($"User with id '{userId}' does not have access to book with id '{bookId}'.");
        }
    }
}

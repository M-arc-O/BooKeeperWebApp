using AutoMapper;
using BooKeeperWebApp.Business.CQRS;
using BooKeeperWebApp.Business.Models;
using BooKeeperWebApp.Infrastructure.Repositories;

namespace BooKeeperWebApp.Business.Queries.BankAccount;
public class GetAllAccountsQueryHandler : IHandler<GetAllAccountsQuery, IEnumerable<BankAccountModel>>
{
    private readonly IGenericRepository<Infrastructure.Entities.BankAccount> _bankAccountRepository;
    private readonly IMapper _mapper;

    public GetAllAccountsQueryHandler(IGenericRepository<Infrastructure.Entities.BankAccount> bankAccountRepository, IMapper mapper)
    {
        _bankAccountRepository = bankAccountRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<BankAccountModel>> ExecuteAsync(GetAllAccountsQuery query)
    {
        var accounts = await _bankAccountRepository.GetAsync(x => x.UserId == query.UserId);
        return accounts.Select(x => _mapper.Map<BankAccountModel>(x));
    }
}

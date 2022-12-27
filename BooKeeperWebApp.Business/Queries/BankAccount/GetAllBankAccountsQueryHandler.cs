using AutoMapper;
using BooKeeperWebApp.Business.CQRS;
using BooKeeperWebApp.Business.Models.Bank;
using BooKeeperWebApp.Infrastructure.Repositories;

namespace BooKeeperWebApp.Business.Queries.BankAccount;
public class GetAllBankAccountsQueryHandler : IHandler<GetAllBankAccountsQuery, IEnumerable<BankAccountModel>>
{
    private readonly IGenericRepository<Infrastructure.Entities.Bank.BankAccount> _bankAccountRepository;
    private readonly IMapper _mapper;

    public GetAllBankAccountsQueryHandler(IGenericRepository<Infrastructure.Entities.Bank.BankAccount> bankAccountRepository, IMapper mapper)
    {
        _bankAccountRepository = bankAccountRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<BankAccountModel>> ExecuteAsync(GetAllBankAccountsQuery query)
    {
        var accounts = await _bankAccountRepository.GetAsync(x => x.UserId == query.UserId);
        return accounts.Select(x => _mapper.Map<BankAccountModel>(x));
    }
}

using AutoMapper;
using BooKeeperWebApp.Business.CQRS;
using BooKeeperWebApp.Business.Models.Investment;
using BooKeeperWebApp.Infrastructure.Repositories;

namespace BooKeeperWebApp.Business.Queries.InvestmentAccount;
public class GetAllInvestmentAccountsQueryHandler : IHandler<GetAllInvestmentAccountsQuery, IEnumerable<InvestmentAccountModel>>
{
    private readonly IGenericRepository<Infrastructure.Entities.Investment.InvestmentAccount> _investmentAccountRepository;
    private readonly IMapper _mapper;

    public GetAllInvestmentAccountsQueryHandler(IGenericRepository<Infrastructure.Entities.Investment.InvestmentAccount> investmentAccountRepository, IMapper mapper)
    {
        _investmentAccountRepository = investmentAccountRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<InvestmentAccountModel>> ExecuteAsync(GetAllInvestmentAccountsQuery query)
    {
        var accounts = await _investmentAccountRepository.GetAsync(x => x.UserId == query.UserId);
        return accounts.Select(x => _mapper.Map<InvestmentAccountModel>(x));
    }
}

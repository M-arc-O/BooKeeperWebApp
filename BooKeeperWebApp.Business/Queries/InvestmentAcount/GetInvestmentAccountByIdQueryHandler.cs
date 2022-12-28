using AutoMapper;
using BooKeeperWebApp.Business.CQRS;
using BooKeeperWebApp.Business.Models.Investment;
using BooKeeperWebApp.Infrastructure.Repositories;
using BooKeeperWebApp.Shared.Exceptions;

namespace BooKeeperWebApp.Business.Queries.InvestmentAccount;
public class GetInvestmentAccountByIdQueryHandler : IHandler<GetInvestmentAccountByIdQuery, InvestmentAccountModel>
{
    private readonly IGenericRepository<Infrastructure.Entities.Investment.InvestmentAccount> _investmentAccountRepository;
    private readonly IMapper _mapper;

    public GetInvestmentAccountByIdQueryHandler(IGenericRepository<Infrastructure.Entities.Investment.InvestmentAccount> investmentAccountRepository, IMapper mapper)
    {
        _investmentAccountRepository = investmentAccountRepository;
        _mapper = mapper;
    }

    public async Task<InvestmentAccountModel> ExecuteAsync(GetInvestmentAccountByIdQuery query)
    {
        var accounts = await _investmentAccountRepository.GetAsync(x => x.UserId == query.UserId, null, "Investments,Investments.Values");
        var account = accounts.FirstOrDefault(x => x.Id == query.AccountId) 
            ?? throw new NotFoundException($"Account with id '{query.AccountId}' could not be found.");
    
        return _mapper.Map<InvestmentAccountModel>(account);
    }
}

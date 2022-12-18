using AutoMapper;
using BooKeeperWebApp.Business.CQRS;
using BooKeeperWebApp.Business.Models;
using BooKeeperWebApp.Infrastructure.Repositories;
using BooKeeperWebApp.Shared.Exceptions;

namespace BooKeeperWebApp.Business.Queries.BankAccount;
public class GetAccountByIdQueryHandler : IHandler<GetAccountByIdQuery, BankAccountModel>
{
    private readonly IGenericRepository<Infrastructure.Entities.BankAccount> _bankAccountRepository;
    private readonly IMapper _mapper;

    public GetAccountByIdQueryHandler(IGenericRepository<Infrastructure.Entities.BankAccount> bankAccountRepository, IMapper mapper)
    {
        _bankAccountRepository = bankAccountRepository;
        _mapper = mapper;
    }

    public async Task<BankAccountModel> ExecuteAsync(GetAccountByIdQuery query)
    {
        var accounts = await _bankAccountRepository.GetAsync(x => x.UserId == query.UserId, null, "Mutations");
        var account = accounts.FirstOrDefault(x => x.Id == query.AccountId) 
            ?? throw new NotFoundException($"Account with id '{query.AccountId}' could not be found.");
    
        return _mapper.Map<BankAccountModel>(account);
    }
}

using BooKeeperWebApp.Infrastructure;
using BooKeeperWebApp.Infrastructure.Extensions;
using BooKeeperWebApp.Infrastructure.Repositories;
using BooKeeperWebApp.Shared;

namespace BooKeeperWebApp.Business;
public class BankAccountBusiness : IBankAccountBusiness
{
    private readonly IGenericRepository<Infrastructure.Entities.BankAccount> _bankAccountRepository;
    private readonly IUnitOfWork _unitOfWork;

    public BankAccountBusiness(IGenericRepository<Infrastructure.Entities.BankAccount> bankAccountRepository, IUnitOfWork unitOfWork)
    {
        _bankAccountRepository = bankAccountRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<BankAccount>> GetBankAccounts()
    {
        var accounts = await _bankAccountRepository.Get();
        return accounts.Select(x => x.ToSharedBankAccount());
    }

    public async Task AddBankAccount(BankAccount bankAccount)
    {


        var bankAccountEntity = new Infrastructure.Entities.BankAccount(
            Guid.NewGuid(),
            bankAccount.Name!
        );
        await _bankAccountRepository.Insert(bankAccountEntity);

        await _unitOfWork.CommitAsync();
    }
}

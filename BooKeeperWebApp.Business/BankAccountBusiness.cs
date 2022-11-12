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

    public IEnumerable<BankAccount> GetBankAccounts()
    {
        return _bankAccountRepository.Get().Select(x => x.ToSharedBankAccount());
    }

    public async Task AddBankAccount(BankAccount bankAccount)
    {
        var bankAccountEntity = new Infrastructure.Entities.BankAccount(
            Guid.NewGuid(),
            bankAccount.Name
        );
        _bankAccountRepository.Insert(bankAccountEntity);

        await _unitOfWork.CommitAsync();
    }
}

using BooKeeperWebApp.Shared;

namespace BooKeeperWebApp.Business;
public interface IBankAccountBusiness
{
    Task AddBankAccount(BankAccount bankAccount);
    Task<IEnumerable<BankAccount>> GetBankAccounts();
}
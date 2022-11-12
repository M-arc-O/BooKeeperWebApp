using BooKeeperWebApp.Shared;

namespace BooKeeperWebApp.Business;
public interface IBankAccountBusiness
{
    Task AddBankAccount(BankAccount bankAccount);
    IEnumerable<BankAccount> GetBankAccounts();
}
using BooKeeperWebApp.Shared.Dtos.Bank;
using BooKeeperWebApp.Shared.Models.Bank;
using Radzen;

namespace Client.Services.Bank;

public class BankAccountService : HttpServiceBase<BankAccountDto, AddBankAccountModel>
{
    public BankAccountService(HttpClient httpClient, NotificationService notificationService)
        : base(httpClient, notificationService, "/api/bankaccount/")
    {

    }

    public async Task<bool> CreateBankAccountAsync(BankAccountDto account)
    {
        var bankAccount = new AddBankAccountModel(account.Name!, account.Number!, account.Type, account.StartAmount, 0);
        return await CreateAsync(bankAccount);
    }

    public async Task<bool> UpdateBankAccountAsync(BankAccountDto account)
    {
        var bankAccount = new AddBankAccountModel(account.Name!, account.Number!, account.Type, account.StartAmount, 0);
        return await UpdateAsync(bankAccount, account.Id);
    }
}

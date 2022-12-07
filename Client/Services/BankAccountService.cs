using BooKeeperWebApp.Shared.Dtos;
using BooKeeperWebApp.Shared.Models;
using System.Net.Http.Json;

namespace Client.Services;

public class BankAccountService
{
    private readonly HttpClient _httpClient;

    public bool Loading;
    public IList<BankAccountDto> Accounts = new List<BankAccountDto>();
    public event Action? RefreshRequested;

    public BankAccountService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task GetBankAccountsAsync()
    {
        Loading = true;
        Accounts = await _httpClient.GetFromJsonAsync<List<BankAccountDto>>("/api/bankaccount/getall") ?? new List<BankAccountDto>();
        RefreshRequested?.Invoke(); 
        Loading = false;
    }

    public async Task<string> CreateBankAccountAsync(BankAccountDto account)
    {
        var bankAccount = new AddBankAccountModel(account.Name!, account.Number!, account.Type, account.StartAmount, 0);
        var result = await _httpClient.PostAsJsonAsync("/api/bankaccount/create", bankAccount);
        return await HandleResult(result);
    }

    public async Task<string> UpdateBankAccountAsync(BankAccountDto account)
    {
        var bankAccount = new AddBankAccountModel(account.Name!, account.Number!, account.Type, account.StartAmount, 0);
        var result = await _httpClient.PutAsJsonAsync($"/api/bankaccount/{account.Id}/update", bankAccount);
        return await HandleResult(result);
    }

    public async Task<string> DeleteBankAccountAsync(Guid id)
    {
        var result = await _httpClient.DeleteAsync($"/api/bankaccount/{id}/delete");
        return await HandleResult(result);
    }

    private static async Task<string> HandleResult(HttpResponseMessage result)
    {
        if (!result.IsSuccessStatusCode)
        {
            var error = await result.Content.ReadFromJsonAsync<ErrorDto>();
            return error?.Message ?? "Something went wrong, not sure what.";
        }

        return string.Empty;
    }
}

using BooKeeperWebApp.Shared.Dtos;
using BooKeeperWebApp.Shared.Dtos.Overview;
using Radzen;
using System.Net.Http.Json;

namespace Client.Services;

public class OverviewService
{
    protected readonly HttpClient _httpClient;
    protected readonly NotificationService _notifactionService;
    protected readonly string _baseUrl;

    public bool LoadingAccounts;
    public List<OverviewAccountDto> Accounts = new();

    public bool LoadingBooks;
    public List<OverviewBookDto> Books = new();

    public event Action? RefreshRequested;

    public OverviewService(HttpClient httpClient, NotificationService notificationService)
    {
        _httpClient = httpClient;
        _notifactionService = notificationService;
        _baseUrl = "/api/overview/";
    }

    public virtual async Task LoadAccountsAsync()
    {
        LoadingAccounts = true;
        Accounts = await GetItems<OverviewAccountDto>("getaccounts") ?? new();
        LoadingAccounts = false;
        RefreshRequested?.Invoke();
    }

    public virtual async Task LoadBooksAsync(int year)
    {
        LoadingBooks = true;
        Books = await GetItems<OverviewBookDto>($"getbooks/{year}") ?? new();
        LoadingBooks = false;
        RefreshRequested?.Invoke();
    }

    private async Task<List<T>?> GetItems<T>(string route)
    {
        var response = await _httpClient.GetAsync($"{_baseUrl}{route}");
        
        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadFromJsonAsync<ErrorDto>();
            _notifactionService.Notify(new NotificationMessage
            {
                Severity = NotificationSeverity.Error,
                Summary = "Error: ",
                Detail = error?.Message ?? "Something went wrong, not sure what.",
                Duration = 4000
            });

            LoadingBooks = false;
        }

        return await response.Content.ReadFromJsonAsync<List<T>>();
    }
}

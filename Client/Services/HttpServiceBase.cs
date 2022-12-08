﻿using BooKeeperWebApp.Shared.Dtos;
using Radzen;
using System.Net.Http.Json;

namespace Client.Services;

public enum ActionType
{
    Create,
    Update,
    Delete
}

public abstract class HttpServiceBase<DtoType, ModelType> where DtoType : IBaseDto
{
    private readonly HttpClient _httpClient;
    private readonly NotificationService _notifactionService;
    private readonly string _baseUrl;

    public bool Loading;
    public bool Creating;
    public bool Updating;
    public bool Deleting;
    public IList<DtoType> Items = new List<DtoType>();
    public event Action? RefreshRequested;

    public bool Busy => Loading || Creating || Updating || Deleting;

    protected HttpServiceBase(HttpClient httpClient, NotificationService notificationService, string baseUrl)
    {
        _httpClient = httpClient;
        _notifactionService = notificationService;
        _baseUrl = baseUrl;
    }

    public virtual async Task LoadAsync()
    {
        Loading = true;
        Items = await _httpClient.GetFromJsonAsync<List<DtoType>>($"{_baseUrl}getall") ?? new List<DtoType>();
        Loading = false;
        RefreshRequested?.Invoke();
    }

    public virtual async Task<bool> CreateAsync(ModelType item)
    {
        Creating = true;
        var result = await _httpClient.PostAsJsonAsync($"{_baseUrl}create", item);
        return await HandleResult(result, ActionType.Create);
    }

    public virtual async Task<bool> UpdateAsync(ModelType item, Guid id)
    {
        Updating = true;
        var result = await _httpClient.PutAsJsonAsync($"{_baseUrl}{id}/update", item);
        return await HandleResult(result, ActionType.Update);
    }

    public virtual async Task<bool> DeleteAsync(Guid id)
    {
        Deleting = true;
        var result = await _httpClient.DeleteAsync($"{_baseUrl}{id}/delete");
        return await HandleResult(result, ActionType.Delete);
    }

    private async Task<bool> HandleResult(HttpResponseMessage response, ActionType type)
    {
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

            Creating = Updating = Deleting = false;

            return false;
        }

        switch (type)
        {
            case ActionType.Create:
                var result = await response.Content.ReadFromJsonAsync<DtoType>();
                Items.Add(result!);
                Creating = false;
                break;
            case ActionType.Update:
                result = await response.Content.ReadFromJsonAsync<DtoType>();
                var item = Items.First(x => x.Id == result!.Id);
                item = result;
                Updating = false;
                break;
            case ActionType.Delete:
                Deleting = false;
                break;
            default:
                break;
        }

        RefreshRequested?.Invoke();

        return true;
    }
}

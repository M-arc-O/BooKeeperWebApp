using BooKeeperWebApp.Shared.Dtos;
using BooKeeperWebApp.Shared.Models;
using Radzen;
using System.Net.Http.Json;

namespace Client.Services;

public class MutationService : HttpServiceBase<MutationDto, AddMutationModel>
{
    public MutationService(HttpClient httpClient, NotificationService notificationService) 
        : base(httpClient, notificationService, "/api/mutation/")
    {
    }

    public async Task<bool> CreateMutationAsync(MutationDto dto)
    {
        var mutation = new AddMutationModel 
        {
            Date = dto.Date,
            AccountNumber = dto.AccountNumber,
            OtherAccountNumber = dto.OtherAccountNumber,
            Description = dto.Description,
            Comment = dto.Comment,
            Tag = dto.Tag,
            Amount = dto.Amount,
            AmountAfterMutation = dto.AmountAfterMutation,
            AccountId = dto.AccountId,
            BookId = dto.BookId,
            EventId = dto.EventId
        };
        return await CreateAsync(mutation);
    }

    public async Task<bool> UpdateMutationAsync(MutationDto dto)
    {
        var mutation = new AddMutationModel
        {
            Date = dto.Date,
            AccountNumber = dto.AccountNumber,
            OtherAccountNumber = dto.OtherAccountNumber,
            Description = dto.Description,
            Comment = dto.Comment,
            Tag = dto.Tag,
            Amount = dto.Amount,
            AmountAfterMutation = dto.AmountAfterMutation,
            AccountId = dto.AccountId,
            BookId = dto.BookId,
            EventId = dto.EventId
        };
        return await UpdateAsync(mutation, dto.Id);
    }

    public async Task<bool> CreateMultipleMutationsAsync(AddMutationModel[] models)
    {
        Creating = true;
        var result = await _httpClient.PostAsJsonAsync($"{_baseUrl}createmultiple", models);
        return await HandleResult(result, ActionType.Get);
    }
}

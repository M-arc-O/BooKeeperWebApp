using BooKeeperWebApp.Shared.Dtos.Bank;
using BooKeeperWebApp.Shared.Models.Bank;
using Radzen;
using System.Net.Http.Json;

namespace Client.Services;

public class MutationService : HttpServiceBase<MutationDto, AddMutationModel>
{
    public MutationService(HttpClient httpClient, NotificationService notificationService) 
        : base(httpClient, notificationService, "/api/mutation/")
    {
    }

    public async Task<bool> CreateMutationAsync(MutationDto dto, Guid bookId, Guid? eventId)
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
            BookId = bookId,
            EventId = eventId
        };
        return await CreateAsync(mutation);
    }

    public async Task<bool> UpdateMutationAsync(MutationDto dto, Guid bookId, Guid? eventId)
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
            BookId = bookId,
            EventId = eventId
        };
        return await UpdateAsync(mutation, dto.Id);
    }

    public async Task<bool> CreateMultipleMutationsAsync(AddMutationModel[] models)
    {
        Creating = true;
        var result = await _httpClient.PostAsJsonAsync($"{_baseUrl}createmultiple", models);
        return await HandleResult(result, (ActionType)(-1));
    }
}

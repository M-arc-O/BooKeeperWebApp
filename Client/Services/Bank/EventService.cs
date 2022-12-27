using BooKeeperWebApp.Shared.Dtos.Bank;
using BooKeeperWebApp.Shared.Models.Bank;
using Radzen;

namespace Client.Services.Bank;

public class EventService : HttpServiceBase<EventDto, AddEventModel>
{
    public EventService(HttpClient httpClient, NotificationService notificationService)
        : base(httpClient, notificationService, "/api/event/")
    {
    }

    public async Task<bool> CreateEventAsync(EventDto dto)
    {
        var newEvent = new AddEventModel(dto.Name!);
        return await CreateAsync(newEvent);
    }

    public async Task<bool> UpdateEventAsync(EventDto dto)
    {
        var updateEvent = new AddEventModel(dto.Name!);
        return await UpdateAsync(updateEvent, dto.Id);
    }
}

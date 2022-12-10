using BooKeeperWebApp.Shared.Dtos;
using BooKeeperWebApp.Shared.Models;
using Radzen;

namespace Client.Services;

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

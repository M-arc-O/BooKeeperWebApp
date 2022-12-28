using AutoMapper;
using BooKeeperWebApp.Business.CQRS;
using BooKeeperWebApp.Business.Models.Bank;
using BooKeeperWebApp.Infrastructure.Repositories;
using BooKeeperWebApp.Shared.Exceptions;

namespace BooKeeperWebApp.Business.Commands.Event;
public class AddEventCommandHandler : EventCommandBase, IHandler<AddEventCommand, EventModel>
{
    private readonly IMapper _mapper;

    public AddEventCommandHandler(IGenericRepository<Infrastructure.Entities.Bank.Event> eventRepository, IMapper mapper)
        : base(eventRepository)
    {
        _mapper = mapper;
    }

    public async Task<EventModel> ExecuteAsync(AddEventCommand command)
    {
        var entitie = new Infrastructure.Entities.Bank.Event
        {
            Id = Guid.NewGuid(),
            UserId = command.UserId,
            Name = command.Name
        };

        ValidateName(command.Name);

        if (await NameTakenAsync(command.Name))
        {
            throw new ValidationException($"Event with name '{command.Name}' already exists");
        }

        await _eventRepository.InsertAsync(entitie);

        return _mapper.Map<EventModel>(entitie);
    }
}

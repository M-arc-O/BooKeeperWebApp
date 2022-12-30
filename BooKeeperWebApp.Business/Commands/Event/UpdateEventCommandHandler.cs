using AutoMapper;
using BooKeeperWebApp.Business.CQRS;
using BooKeeperWebApp.Business.Models.Bank;
using BooKeeperWebApp.Infrastructure.Repositories;
using BooKeeperWebApp.Shared.Exceptions;

namespace BooKeeperWebApp.Business.Commands.Event;
public class UpdateEventCommandHandler : EventCommandBase, IHandler<UpdateEventCommand, EventModel>
{
    private readonly IMapper _mapper;

    public UpdateEventCommandHandler(IGenericRepository<Infrastructure.Entities.Bank.Event> eventRepository, IMapper mapper)
        : base(eventRepository)
    {
        _mapper = mapper;
    }

    public async Task<EventModel> ExecuteAsync(UpdateEventCommand command)
    {
        var entitie = await GetEventAsync(command.UserId, command.EventId);

        ValidateName(command.Name);

        if (command.Name != entitie.Name && await NameTakenAsync(command.UserId, command.Name))
        {
            throw new ValidationException($"Account with number '{command.Name}' already exists");
        }

        entitie.Name = command.Name;
        _eventRepository.Update(entitie);

        return _mapper.Map<EventModel>(entitie);
    }
}

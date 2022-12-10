using BooKeeperWebApp.Business.CQRS;

namespace BooKeeperWebApp.Business.Commands.Event;
public class AddEventCommand : ICommand
{
    public Guid UserId { get; }
    public string Name { get; }

    public AddEventCommand(Guid userId, string name)
    {
        UserId = userId;
        Name = name;
    }
}

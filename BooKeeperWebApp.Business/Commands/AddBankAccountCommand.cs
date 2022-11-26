using BooKeeperWebApp.Business.CQRS;

namespace BooKeeperWebApp.Business.Commands;
public class AddBankAccountCommand : ICommand
{
	public Guid UserId { get; set; }
	public string AccountName { get; }

	public AddBankAccountCommand(Guid userId, string accountName)
	{
		UserId = userId;
		AccountName = accountName;
	}
}

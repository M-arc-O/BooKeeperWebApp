using BooKeeperWebApp.Business.CQRS;
using BooKeeperWebApp.Infrastructure.Repositories;
using BooKeeperWebApp.Shared.Exceptions;

namespace BooKeeperWebApp.Business.Commands.InvestmentValue;
public class DeleteInvestmentValueCommandHandler : InvestmentValueCommandBase, IHandler<DeleteInvestmentValueCommand, Guid>
{
    public DeleteInvestmentValueCommandHandler(
        IGenericRepository<Infrastructure.Entities.Investment.InvestmentValue> investmentValueRepository,
        IGenericRepository<Infrastructure.Entities.Investment.Investment> investmentRepository,
        IGenericRepository<Infrastructure.Entities.Investment.InvestmentAccount> investmentAccountRepository)
        : base(investmentValueRepository, investmentRepository, investmentAccountRepository)
    {
    }

    public async Task<Guid> ExecuteAsync(DeleteInvestmentValueCommand command)
    {
        var investmentValue = await GetInvestmentValueAsync(command.UserId, command.InvestmentValuetId)
            ?? throw new NotFoundException($"Value with id '{command.InvestmentValuetId}' not found.");

        _investmentValueRepository.Delete(investmentValue);

        return command.InvestmentValuetId;
    }
}

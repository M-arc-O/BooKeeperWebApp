using BooKeeperWebApp.Business.CQRS;
using BooKeeperWebApp.Infrastructure.Repositories;
using BooKeeperWebApp.Shared.Exceptions;

namespace BooKeeperWebApp.Business.Commands.Investment;
public class DeleteInvestmentCommandHandler : InvestmentCommandBase, IHandler<DeleteInvestmentCommand, Guid>
{
    private readonly IGenericRepository<Infrastructure.Entities.Investment.InvestmentValue> _investmentValueRepository;

    public DeleteInvestmentCommandHandler(
        IGenericRepository<Infrastructure.Entities.Investment.InvestmentAccount> investmentAccountRepository,
        IGenericRepository<Infrastructure.Entities.Investment.Investment> investmentRepository,
        IGenericRepository<Infrastructure.Entities.Investment.InvestmentValue> investmentValueRepository)
        : base(investmentRepository, investmentAccountRepository)
    {
        _investmentValueRepository = investmentValueRepository;
    }

    public async Task<Guid> ExecuteAsync(DeleteInvestmentCommand command)
    {
        var investments = await _investmentRepository.GetAsync(
            x => x.Id == command.InvestmentId,
            null,
            "Values");
        var investment = investments.FirstOrDefault() ?? throw new NotFoundException($"Investment account with id '{command.InvestmentId}' not found.");

        await CheckIfUserHasAccesToInvestment(command.UserId, investment);

        var latestValue = investment.Values.OrderByDescending(x => x.Date).FirstOrDefault();

        if (latestValue != null)
        {
            var account = await _investmentAccountRepository.GetByIdAsync(investment!.InvestmentAccountId);
            account!.CurrentAmount -= latestValue.Value;
        }

        Parallel.ForEach(investment.Values!, (value) =>
        {
            _investmentValueRepository.Delete(value);
        });

        _investmentRepository.Delete(investment);

        return command.InvestmentId;
    }
}

using BooKeeperWebApp.Business.CQRS;
using BooKeeperWebApp.Infrastructure.Repositories;
using BooKeeperWebApp.Shared.Exceptions;

namespace BooKeeperWebApp.Business.Commands.InvestmentAccount;
public class DeleteInvestmentAccountCommandHandler : InvestmentAccountCommandBase, IHandler<DeleteInvestmentAccountCommand, Guid>
{
    private readonly IGenericRepository<Infrastructure.Entities.Investment.InvestmentAccount> _investmentAccountRepository;
    private readonly IGenericRepository<Infrastructure.Entities.Investment.Investment> _investmentRepository;
    private readonly IGenericRepository<Infrastructure.Entities.Investment.InvestmentValue> _investmentValueRepository;

    public DeleteInvestmentAccountCommandHandler(
        IGenericRepository<Infrastructure.Entities.Investment.InvestmentAccount> investmentAccountRepository,
        IGenericRepository<Infrastructure.Entities.Investment.Investment> investmentRepository,
        IGenericRepository<Infrastructure.Entities.Investment.InvestmentValue> investmentValueRepository)
        : base(investmentAccountRepository)
    {
        _investmentAccountRepository = investmentAccountRepository;
        _investmentRepository = investmentRepository;
        _investmentValueRepository = investmentValueRepository;
    }

    public async Task<Guid> ExecuteAsync(DeleteInvestmentAccountCommand command)
    {
        var investmentAccounts = await _investmentAccountRepository.GetAsync(x => x.Id == command.AccountId && x.UserId == command.UserId, null, "Investments, Investments.Values");
        var investmentAccount = investmentAccounts.FirstOrDefault() ?? throw new NotFoundException($"Investment account with id '{command.AccountId}' not found.");

        Parallel.ForEach(investmentAccount.Investments!, (investment) =>
        {
            foreach(var value in investment.Values)
            {
                _investmentValueRepository.Delete(value);
            }
            _investmentRepository.Delete(investment);
        });

        _investmentAccountRepository.Delete(investmentAccount);

        return command.AccountId;
    }
}

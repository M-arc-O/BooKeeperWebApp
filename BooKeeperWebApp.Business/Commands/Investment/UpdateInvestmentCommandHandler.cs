using AutoMapper;
using BooKeeperWebApp.Business.CQRS;
using BooKeeperWebApp.Business.Models.Investment;
using BooKeeperWebApp.Infrastructure.Repositories;
using BooKeeperWebApp.Shared.Exceptions;

namespace BooKeeperWebApp.Business.Commands.Investment;
public class UpdateInvestmentCommandHandler : InvestmentCommandBase, IHandler<UpdateInvestmentCommand, InvestmentModel>
{
    private readonly IMapper _mapper;

    public UpdateInvestmentCommandHandler(
        IGenericRepository<Infrastructure.Entities.Investment.Investment> investmentRepository,
        IGenericRepository<Infrastructure.Entities.Investment.InvestmentAccount> investmentAccountRepository,
        IMapper mapper)
        : base(investmentRepository, investmentAccountRepository)
    {
        _mapper = mapper;
    }

    public async Task<InvestmentModel> ExecuteAsync(UpdateInvestmentCommand command)
    {
        var investment = await GetInvestmentAsync(command.UserId, command.InvestmentId);

        if (command.Name != investment.Name && await NameTakenAsync(investment.InvestmentAccountId, command.Name))
        {
            throw new ValidationException($"Account with name '{command.Name}' already exists");
        }

        investment.Name = command.Name;
        _investmentRepository.Update(investment);

        return _mapper.Map<InvestmentModel>(investment);
    }
}

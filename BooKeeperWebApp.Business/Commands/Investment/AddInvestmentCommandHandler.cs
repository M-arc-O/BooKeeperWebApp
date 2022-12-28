using AutoMapper;
using BooKeeperWebApp.Business.CQRS;
using BooKeeperWebApp.Business.Models.Investment;
using BooKeeperWebApp.Infrastructure.Repositories;
using BooKeeperWebApp.Shared.Exceptions;

namespace BooKeeperWebApp.Business.Commands.Investment;
public class AddInvestmentCommandHandler : InvestmentCommandBase, IHandler<AddInvestmentCommand, InvestmentModel>
{
    private readonly IMapper _mapper;

    public AddInvestmentCommandHandler(
        IGenericRepository<Infrastructure.Entities.Investment.Investment> investmentRepository,
        IGenericRepository<Infrastructure.Entities.Investment.InvestmentAccount> investmentAccountRepository,
        IMapper mapper)
        : base(investmentRepository, investmentAccountRepository)
    {
        _mapper = mapper;
    }

    public async Task<InvestmentModel> ExecuteAsync(AddInvestmentCommand command)
    {
        var investment = new Infrastructure.Entities.Investment.Investment
        {
            Id = Guid.NewGuid(),
            InvestmentAccountId = command.InvestmentAccountId,
            Name = command.Name
        };

        if (await NameTakenAsync(command.Name))
        {
            throw new ValidationException($"Investment with name '{command.Name}' already exists");
        }

        await _investmentRepository.InsertAsync(investment);

        return _mapper.Map<InvestmentModel>(investment);
    }
}

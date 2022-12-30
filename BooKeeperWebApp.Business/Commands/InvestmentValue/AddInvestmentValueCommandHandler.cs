using AutoMapper;
using BooKeeperWebApp.Business.CQRS;
using BooKeeperWebApp.Business.Models.Investment;
using BooKeeperWebApp.Infrastructure.Enums;
using BooKeeperWebApp.Infrastructure.Repositories;
using BooKeeperWebApp.Shared.Exceptions;

namespace BooKeeperWebApp.Business.Commands.InvestmentValue;
public class AddInvestmentValueCommandHandler : InvestmentValueCommandBase, IHandler<AddInvestmentValueCommand, InvestmentValueModel>
{
    private readonly IMapper _mapper;

    public AddInvestmentValueCommandHandler(
        IGenericRepository<Infrastructure.Entities.Investment.InvestmentValue> investmentValueRepository,
        IGenericRepository<Infrastructure.Entities.Investment.Investment> investmentRepository,
        IGenericRepository<Infrastructure.Entities.Investment.InvestmentAccount> investmentAccountRepository
        , IMapper mapper)
        : base(investmentValueRepository, investmentRepository, investmentAccountRepository)
    {
        _mapper = mapper;
    }

    public async Task<InvestmentValueModel> ExecuteAsync(AddInvestmentValueCommand command)
    {
        var investmentValue = new Infrastructure.Entities.Investment.InvestmentValue
        {
            Id = Guid.NewGuid(),
            InvestmentId = command.InvestmentId,
            Date = command.Date,
            Value = command.Value
        };

        if (await DateTakenAsync(command.InvestmentId, command.Date))
        {
            throw new ValidationException($"Value with date '{command.Date:dd-MM-yyyy}' already exists");
        }

        var account = await GetInvestmentAccount(investmentValue.InvestmentId);
        var values = await GetInvestmentValues(investmentValue.InvestmentId);
        var latestValue = values.FirstOrDefault();

        if (latestValue != null)
        {
            if (investmentValue.Date.Date > latestValue.Date.Date)
            {
                account!.CurrentAmount -= latestValue.Value;
                account!.CurrentAmount += investmentValue.Value;
            }
        }     
        else
        {
            account!.CurrentAmount += investmentValue.Value;
        }
        
        await _investmentValueRepository.InsertAsync(investmentValue);

        return _mapper.Map<InvestmentValueModel>(investmentValue);
    }
}

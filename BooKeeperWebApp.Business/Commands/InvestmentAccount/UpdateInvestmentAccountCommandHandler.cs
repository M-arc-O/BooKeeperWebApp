using AutoMapper;
using BooKeeperWebApp.Business.CQRS;
using BooKeeperWebApp.Business.Models.Investment;
using BooKeeperWebApp.Infrastructure.Enums;
using BooKeeperWebApp.Infrastructure.Repositories;
using BooKeeperWebApp.Shared.Exceptions;

namespace BooKeeperWebApp.Business.Commands.InvestmentAccount;
public class UpdateInvestmentAccountCommandHandler : InvestmentAccountCommandBase, IHandler<UpdateInvestmentAccountCommand, InvestmentAccountModel>
{
    private readonly IMapper _mapper;

    public UpdateInvestmentAccountCommandHandler(IGenericRepository<Infrastructure.Entities.Investment.InvestmentAccount> investmentAccountRepository, IMapper mapper)
        : base(investmentAccountRepository)
    {
        _mapper = mapper;
    }

    public async Task<InvestmentAccountModel> ExecuteAsync(UpdateInvestmentAccountCommand command)
    {
        var investmentAccount = await GetInvestmentAccountAsync(command.UserId, command.AccountId);

        if (command.Name != investmentAccount.Name && await NameTakenAsync(command.Name))
        {
            throw new ValidationException($"Account with name '{command.Name}' already exists");
        }

        investmentAccount.Name = command.Name;
        investmentAccount.Type = (InvestmentAccountType)command.Type;
        _investmentAccountRepository.Update(investmentAccount);

        return _mapper.Map<InvestmentAccountModel>(investmentAccount);
    }
}

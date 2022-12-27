using AutoMapper;
using BooKeeperWebApp.Business.CQRS;
using BooKeeperWebApp.Business.Models.Investment;
using BooKeeperWebApp.Infrastructure.Enums;
using BooKeeperWebApp.Infrastructure.Repositories;
using BooKeeperWebApp.Shared.Exceptions;

namespace BooKeeperWebApp.Business.Commands.InvestmentAccount;
public class AddInvestmentAccountCommandHandler : InvestmentAccountCommandBase, IHandler<AddInvestmentAccountCommand, InvestmentAccountModel>
{
    private readonly IGenericRepository<Infrastructure.Entities.Investment.InvestmentAccount> _investmentAccountRepository;
    private readonly IMapper _mapper;

    public AddInvestmentAccountCommandHandler(IGenericRepository<Infrastructure.Entities.Investment.InvestmentAccount> investmentAccountRepository, IMapper mapper)
        : base(investmentAccountRepository)
    {
        _investmentAccountRepository = investmentAccountRepository;
        _mapper = mapper;
    }

    public async Task<InvestmentAccountModel> ExecuteAsync(AddInvestmentAccountCommand command)
    {
        var investmentAccount = new Infrastructure.Entities.Investment.InvestmentAccount
        {
            Id = Guid.NewGuid(),
            UserId = command.UserId,
            Name = command.Name,
            Type = (InvestmentAccountType)command.Type
        };

        if (await NameTakenAsync(command.Name))
        {
            throw new ValidationException($"Account with name '{command.Name}' already exists");
        }

        await _investmentAccountRepository.InsertAsync(investmentAccount);

        return _mapper.Map<InvestmentAccountModel>(investmentAccount);
    }
}

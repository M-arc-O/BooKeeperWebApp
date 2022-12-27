using BooKeeperWebApp.Shared.Enums;

namespace BooKeeperWebApp.Shared.Models.Investment;
public record AddInvestmentAccountModel(string Name, InvestmentAccountType Type);

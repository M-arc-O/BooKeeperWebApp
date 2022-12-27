using BooKeeperWebApp.Shared.Enums;

namespace BooKeeperWebApp.Shared.Models.Bank;
public record AddBankAccountModel(string Name, string Number, BankAccountType Type, double StartAmount, double CurrentAmount);

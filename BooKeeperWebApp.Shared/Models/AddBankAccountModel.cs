﻿using BooKeeperWebApp.Shared.Enums;

namespace BooKeeperWebApp.Shared.Models;
public record AddBankAccountModel(string Name, string Number, AccountType Type, double StartAmount, double CurrentAmount);

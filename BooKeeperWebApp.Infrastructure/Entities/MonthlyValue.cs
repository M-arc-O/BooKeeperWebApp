﻿namespace BooKeeperWebApp.Infrastructure.Entities;
public class MonthlyValue
{
    public Guid Id { get; set; }
    public Guid AccountId { get; set; }
    public DateTime Date { get; set; }
    public double Value { get; set; }
}

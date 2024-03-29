﻿namespace BooKeeperWebApp.Shared.Dtos.Bank;
public class EventDto : IBaseDto
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public ICollection<MutationDto>? Mutations { get; set; }
}

﻿namespace BooKeeperWebApp.Shared.Dtos;
public class BookDto : IBaseDto
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public ICollection<MutationDto>? Mutations { get; set; }
}
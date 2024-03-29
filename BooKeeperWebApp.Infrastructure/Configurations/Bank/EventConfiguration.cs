﻿using BooKeeperWebApp.Infrastructure.Entities.Bank;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BooKeeperWebApp.Infrastructure.Configurations.Bank;
public class EventConfiguration : IEntityTypeConfiguration<Event>
{
    public void Configure(EntityTypeBuilder<Event> builder)
    {
        builder.ToTable("Event");
        builder.HasKey(x => x.Id);

        builder.HasMany(x => x.Mutations)
            .WithOne()
            .HasForeignKey("EventId");
    }
}

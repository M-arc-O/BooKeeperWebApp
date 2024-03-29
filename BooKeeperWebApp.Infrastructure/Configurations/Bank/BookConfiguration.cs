﻿using BooKeeperWebApp.Infrastructure.Entities.Bank;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BooKeeperWebApp.Infrastructure.Configurations.Bank;
public class BookConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.ToTable("Book");
        builder.HasKey(x => x.Id);

        builder.Navigation(x => x.Mutations).AutoInclude();
        builder.HasMany(x => x.Mutations)
            .WithOne()
            .HasForeignKey("BookId");

        builder.HasMany(x => x.MonthlyValues)
            .WithOne()
            .HasForeignKey("AccountId");

        builder.HasMany(x => x.YearlyValues)
            .WithOne()
            .HasForeignKey("AccountId");
    }
}

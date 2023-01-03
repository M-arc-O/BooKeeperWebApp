using BooKeeperWebApp.Infrastructure.Entities.Bank;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BooKeeperWebApp.Infrastructure.Configurations.Bank;
public class BankAccountConfiguration : IEntityTypeConfiguration<BankAccount>
{
    public void Configure(EntityTypeBuilder<BankAccount> builder)
    {
        builder.ToTable("BankAccount");
        builder.HasKey(x => x.Id);

        builder.HasMany(x => x.Mutations)
            .WithOne()
            .HasForeignKey("AccountId");

        builder.HasMany(x => x.MonthlyValues)
            .WithOne()
            .HasForeignKey("AccountId");

        builder.HasMany(x => x.YearlyValues)
            .WithOne()
            .HasForeignKey("AccountId");
    }
}

using BooKeeperWebApp.Infrastructure.Entities.Investment;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BooKeeperWebApp.Infrastructure.Configurations.Investment;
public class InvestmentAccountConfiguration : IEntityTypeConfiguration<InvestmentAccount>
{
    public void Configure(EntityTypeBuilder<InvestmentAccount> builder)
    {
        builder.ToTable("InvestmentAccount");
        builder.HasKey(x => x.Id);

        builder.HasMany(x => x.Investments)
            .WithOne()
            .HasForeignKey("InvestmentAccountId");

        builder.HasMany(x => x.MonthlyValues)
            .WithOne()
            .HasForeignKey("AccountId");

        builder.HasMany(x => x.YearlyValues)
            .WithOne()
            .HasForeignKey("AccountId");
    }
}

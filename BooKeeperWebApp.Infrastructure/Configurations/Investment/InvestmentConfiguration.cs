using BooKeeperWebApp.Infrastructure.Entities.Investment;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BooKeeperWebApp.Infrastructure.Configurations.Investment;
public class InvestmentConfiguration : IEntityTypeConfiguration<Entities.Investment.Investment>
{
    public void Configure(EntityTypeBuilder<Entities.Investment.Investment> builder)
    {
        builder.ToTable("Investment");
        builder.HasKey(x => x.Id);

        builder.HasMany(x => x.Values)
            .WithOne()
            .HasForeignKey("InvestmentId");
    }
}

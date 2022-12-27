using BooKeeperWebApp.Infrastructure.Entities.Investment;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BooKeeperWebApp.Infrastructure.Configurations;
public class InvestmentConfiguration : IEntityTypeConfiguration<Investment>
{
    public void Configure(EntityTypeBuilder<Investment> builder)
    {
        builder.ToTable("Investment");
        builder.HasKey(x => x.Id);

        builder.HasMany(x => x.Values)
            .WithOne()
            .HasForeignKey("InvestmentId");
    }
}

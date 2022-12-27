using BooKeeperWebApp.Infrastructure.Entities.Investment;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BooKeeperWebApp.Infrastructure.Configurations.Investment;
public class InvestmentValueConfiguration : IEntityTypeConfiguration<InvestmentValue>
{
    public void Configure(EntityTypeBuilder<InvestmentValue> builder)
    {
        builder.ToTable("InvestmentValue");
        builder.HasKey(x => x.Id);
    }
}

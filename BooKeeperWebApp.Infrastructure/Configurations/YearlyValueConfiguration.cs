using BooKeeperWebApp.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BooKeeperWebApp.Infrastructure.Configurations.Investment;
public class YearlyValueConfiguration : IEntityTypeConfiguration<YearlyValue>
{
    public void Configure(EntityTypeBuilder<YearlyValue> builder)
    {
        builder.ToTable("YearlyValue");
        builder.HasKey(x => x.Id);
    }
}

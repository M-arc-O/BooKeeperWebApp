using BooKeeperWebApp.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BooKeeperWebApp.Infrastructure.Configurations;
public class MonthlyValueConfiguration : IEntityTypeConfiguration<MonthlyValue>
{
    public void Configure(EntityTypeBuilder<MonthlyValue> builder)
    {
        builder.ToTable("MonthlyValue");
        builder.HasKey(x => x.Id);
    }
}

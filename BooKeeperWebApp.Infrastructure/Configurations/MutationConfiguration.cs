using BooKeeperWebApp.Infrastructure.Entities.Bank;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BooKeeperWebApp.Infrastructure.Configurations;
public class MutationConfiguration : IEntityTypeConfiguration<Mutation>
{
    public void Configure(EntityTypeBuilder<Mutation> builder)
    {
        builder.ToTable("Mutation");
        builder.HasKey(x => x.Id);

        builder.Navigation(x => x.Account).AutoInclude();
        builder.HasOne(x => x.Account)
            .WithMany(x => x.Mutations)
            .HasForeignKey("AccountId")
            .IsRequired();

        builder.Navigation(x => x.Book).AutoInclude();
        builder.HasOne(x => x.Book)
            .WithMany(x => x.Mutations)
            .HasForeignKey("BookId")
            .IsRequired();

        builder.Navigation(x => x.Event).AutoInclude();
        builder.HasOne(x => x.Event)
            .WithMany(x => x.Mutations)
            .HasForeignKey("EventId");
    }
}

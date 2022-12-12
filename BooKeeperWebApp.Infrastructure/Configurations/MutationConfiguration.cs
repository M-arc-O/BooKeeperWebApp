using BooKeeperWebApp.Infrastructure.Entities;
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
            .WithMany()
            .HasForeignKey("FK_Mutation_AccountId");

        builder.Navigation(x => x.Book).AutoInclude();
        builder.HasOne(x => x.Book)
            .WithMany()
            .HasForeignKey("FK_Mutation_BookId");

        builder.Navigation(x => x.Event).AutoInclude();
        builder.HasOne(x => x.Event)
            .WithMany()
            .HasForeignKey("FK_Mutation_EventId");
    }
}

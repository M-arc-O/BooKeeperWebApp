using BooKeeperWebApp.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BooKeeperWebApp.Infrastructure.Configurations;
public class BookConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.ToTable("Book");
        builder.HasKey(x => x.Id);

        builder.HasMany(x => x.Mutations)
            .WithOne()
            .HasForeignKey("FK_Mutation_BookId");
    }
}

using BooKeeperWebApp.Infrastructure.Configurations;
using BooKeeperWebApp.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace BooKeeperWebApp.Infrastructure.Contexts;
public class BooKeeperWebAppDbContext : DbContext
{
	public DbSet<BankAccount>? BankAccounts { get; set; }
    public DbSet<User>? Users { get; set; }
    public DbSet<Book>? Books { get; set; }
    public DbSet<Event>? Events { get; set; }

    public BooKeeperWebAppDbContext(DbContextOptions<BooKeeperWebAppDbContext> options) : base(options)
    {
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfiguration(new BankAccountConfiguration());
        modelBuilder.ApplyConfiguration(new BookConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
	}

    public static void ConfigureDbContextOptions(DbContextOptionsBuilder optionsBuilder, string connectionString)
    {
		optionsBuilder
			.UseSqlServer(connectionString,
				sqlServerOptionsAction => sqlServerOptionsAction
					.CommandTimeout(100)
					.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)
					.EnableRetryOnFailure(
						maxRetryCount: 10,
						maxRetryDelay: TimeSpan.FromSeconds(2),
						errorNumbersToAdd: null));
    }
}

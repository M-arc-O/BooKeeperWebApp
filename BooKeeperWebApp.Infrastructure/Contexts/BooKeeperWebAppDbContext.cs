using BooKeeperWebApp.Infrastructure.Configurations;
using BooKeeperWebApp.Infrastructure.Configurations.Bank;
using BooKeeperWebApp.Infrastructure.Configurations.Investment;
using BooKeeperWebApp.Infrastructure.Entities;
using BooKeeperWebApp.Infrastructure.Entities.Bank;
using BooKeeperWebApp.Infrastructure.Entities.Investment;
using Microsoft.EntityFrameworkCore;

namespace BooKeeperWebApp.Infrastructure.Contexts;
public class BooKeeperWebAppDbContext : DbContext
{
	public DbSet<BankAccount>? BankAccounts { get; set; }
    public DbSet<User>? Users { get; set; }
    public DbSet<Book>? Books { get; set; }
    public DbSet<Event>? Events { get; set; }
    public DbSet<Mutation>? Mutations { get; set; }
    public DbSet<InvestmentAccount>? InvestmentAccounts { get; set; }


    public BooKeeperWebAppDbContext(DbContextOptions<BooKeeperWebAppDbContext> options) : base(options)
    {
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfiguration(new BankAccountConfiguration());
        modelBuilder.ApplyConfiguration(new BookConfiguration());
        modelBuilder.ApplyConfiguration(new EventConfiguration());
        modelBuilder.ApplyConfiguration(new MutationConfiguration());

        modelBuilder.ApplyConfiguration(new InvestmentAccountConfiguration());
        modelBuilder.ApplyConfiguration(new InvestmentConfiguration());
        modelBuilder.ApplyConfiguration(new InvestmentValueConfiguration());

        modelBuilder.ApplyConfiguration(new MonthlyValueConfiguration());
        modelBuilder.ApplyConfiguration(new YearlyValueConfiguration());

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

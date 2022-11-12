using BooKeeperWebApp.Infrastructure.Contexts;

namespace BooKeeperWebApp.Infrastructure;
public class UnitOfWork : IUnitOfWork
{
    private readonly BooKeeperWebAppDbContext _dbContext;

    public UnitOfWork(BooKeeperWebAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> CommitAsync()
    {
        return await _dbContext.SaveChangesAsync();
    }
}

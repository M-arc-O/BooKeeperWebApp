namespace BooKeeperWebApp.Infrastructure;

public interface IUnitOfWork
{
    Task<int> CommitAsync();
}
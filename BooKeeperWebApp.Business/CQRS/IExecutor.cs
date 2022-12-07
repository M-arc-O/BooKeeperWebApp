namespace BooKeeperWebApp.Business.CQRS;
public interface IExecutor
{
    Task<TResult> ExecuteAsync<TExecutable, TResult>(TExecutable executable) where TExecutable : IExecutable;
}

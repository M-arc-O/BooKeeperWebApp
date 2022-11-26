namespace BooKeeperWebApp.Business.CQRS;
public interface IExecutor
{
    Task<TResult> Execute<TExecutable, TResult>(TExecutable executable) where TExecutable : IExecutable;
}

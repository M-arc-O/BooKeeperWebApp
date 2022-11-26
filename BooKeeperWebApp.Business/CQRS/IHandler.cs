namespace BooKeeperWebApp.Business.CQRS;
public interface IHandler<TExecutable, TResult> where TExecutable : IExecutable
{
    Task<TResult> ExecuteAsync(TExecutable executable);
}

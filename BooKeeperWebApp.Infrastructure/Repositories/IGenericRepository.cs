using System.Linq.Expressions;

namespace BooKeeperWebApp.Infrastructure.Repositories;
public interface IGenericRepository<TEntity> where TEntity : class
{
    void Delete(object id);
    void Delete(TEntity entityToDelete);
    Task<List<TEntity>> Get(Expression<Func<TEntity, bool>>? filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, string includeProperties = "");
    Task<TEntity?> GetByID(object id);
    Task Insert(TEntity entity);
    void Update(TEntity entityToUpdate);
}

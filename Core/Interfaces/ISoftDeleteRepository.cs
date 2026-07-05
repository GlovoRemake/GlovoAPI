using System.Linq.Expressions;
using Domain.Entities.Base;

namespace Core.Interfaces;

public interface ISoftDeleteRepository<TEntity, TKey>
    where TEntity : class, IEntityWithIsDeleted<TKey>, new()
{
    Task<TEntity?> GetByIdAsync(TKey id, bool IsDelete = false);
    Task<IEnumerable<TEntity>> ListAllAsync(bool IsDelete = false);
    Task AddAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task DeleteAsync(TKey id);
    IQueryable<TEntity> Query();
    Task<int> SaveChangesAsync();
}
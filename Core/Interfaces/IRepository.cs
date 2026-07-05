using System.Linq.Expressions;
using Domain.Entities.Base;

namespace Core.Interfaces;

public interface IRepository<TEntity, TKey>
    where TEntity : class, IEntity<TKey>, new()
{
    Task<TEntity?> GetByIdAsync(TKey id);
    Task<IEnumerable<TEntity>> ListAllAsync();
    Task AddAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task DeleteAsync(TKey id);
    IQueryable<TEntity> Query();
    Task<int> SaveChangesAsync();
}
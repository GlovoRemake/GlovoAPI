using Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Domain.Data;
using Core.Interfaces;
using Domain.Entities.Base;

namespace Core.Repositories;

public class Repository<TEntity, TKey>(GlovoDbContext context) :
    IRepository<TEntity, TKey>
    where TEntity : class, IEntity<TKey>, new()
{
    public async Task<TEntity?> GetByIdAsync(TKey id)
    {
        var entity = await context.Set<TEntity>().FindAsync(id);

        if (entity == null) return null;

        return entity;
    }
    
    public async Task<IEnumerable<TEntity>> ListAllAsync()
    {
        return await context.Set<TEntity>()
            .OrderBy(e => e.Id)
            .ToListAsync();
    }

    public async Task AddAsync(TEntity entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        await context.Set<TEntity>().AddAsync(entity);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(TEntity entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        context.Set<TEntity>().Update(entity);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(TKey id)
    {
        var entity = await context.Set<TEntity>().FindAsync(id);
        if (entity == null) return;

        context.Set<TEntity>().Remove(entity);
        await context.SaveChangesAsync();
    }

    public IQueryable<TEntity> Query()
        => context.Set<TEntity>().AsQueryable();
    
    public async Task<int> SaveChangesAsync() => await context.SaveChangesAsync();
}
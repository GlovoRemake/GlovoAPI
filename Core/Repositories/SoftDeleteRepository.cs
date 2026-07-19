using Microsoft.EntityFrameworkCore;
using Domain.Data;
using Core.Interfaces;
using Domain.Entities.Base;

namespace Core.Repositories;

public class SoftDeleteRepository<TEntity, TKey>(GlovoDbContext context) :
    ISoftDeleteRepository<TEntity, TKey>
    where TEntity : class, IEntityWithIsDeleted<TKey>, new()
{
    public async Task<TEntity?> GetByIdAsync(TKey id, bool isDelete = false)
    {
        var entity = await context.Set<TEntity>().FindAsync(id);

        if (entity == null) return null;

        return entity!.IsDeleted == isDelete ? entity : null;
    }
    
    public async Task<IEnumerable<TEntity>> ListAllAsync(bool isDelete = false)
    {
        return await context.Set<TEntity>()
            .Where(x => x.IsDeleted == isDelete)
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

        entity.IsDeleted = true;
        await context.SaveChangesAsync();
    }

    public async Task ForceDeleteAsync(TKey id)
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
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Interfaces;
using Domain.Data;
using Domain.Entities.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Core.Repositories;

public class SoftDeleteRepository<TEntity, TKey>(GlovoDbContext context, IMapper _mapper) :
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

    public async Task<(IEnumerable<TEntity> Items, int TotalCount)> ListPagedAsync(
        int pageNumber,
        int pageSize,
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        bool descending = false)
    {
        if (pageNumber < 1)
            throw new ArgumentOutOfRangeException(nameof(pageNumber));

        if (pageSize < 1)
            throw new ArgumentOutOfRangeException(nameof(pageSize));

        IQueryable<TEntity> query = context.Set<TEntity>();

        if (predicate != null)
            query = query.Where(predicate);

        if (orderBy != null)
        {
            query = orderBy(query);
        }
        else
        {
            query = query.OrderBy(x => x.Id);
        }

        var totalCount = await query.Where(x => !x.IsDeleted).CountAsync();

        var items = await query
            .Where(x => !x.IsDeleted)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (items, totalCount);
    }

    public async Task<(IEnumerable<TDto> Items, int TotalCount)> ListPagedAsync<TDto>(
        int pageNumber,
        int pageSize,
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null)
    {
        if (pageNumber < 1)
            throw new ArgumentOutOfRangeException(nameof(pageNumber));

        if (pageSize < 1)
            throw new ArgumentOutOfRangeException(nameof(pageSize));

        IQueryable<TEntity> query = context.Set<TEntity>();

        query = query.Where(x => !x.IsDeleted);

        if (predicate != null)
            query = query.Where(predicate);

        if (orderBy != null)
            query = orderBy(query);
        else
            query = query.OrderBy(x => x.Id);

        var totalCount = await query.CountAsync();

        var items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ProjectTo<TDto>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return (items, totalCount);
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

        entity.DateUpdated = DateTime.UtcNow;

        context.Set<TEntity>().Update(entity);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(TKey id)
    {
        var entity = await context.Set<TEntity>().FindAsync(id);
        if (entity == null) return;

        entity.DateUpdated = DateTime.UtcNow;

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
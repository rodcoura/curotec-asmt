using Asmt.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Asmt.DAL.Repositories;

/// <summary>
/// Generic repository implementation for data access operations.
/// </summary>
/// <typeparam name="T">The entity type that implements IAtom interface.</typeparam>
public class GenericRepository<T> : IRepository<T> where T : class, IAtom
{
    protected readonly DbContext _context;
    protected readonly DbSet<T> _dbSet;

    public GenericRepository(DbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    /// <inheritdoc />
    public async Task<T> GetByIdAsync(int id) => await _dbSet.FindAsync(id);

    /// <inheritdoc />
    public async Task<IEnumerable<TProjection>> GetByAsync<TProjection>(Expression<Func<T, TProjection>> selector, Expression<Func<T, bool>>? filter = null, int? skip = null, int? take = null)
        where TProjection : class
    {
        IQueryable<T> query = _dbSet;

        if (filter != null)
            query = query.Where(filter);

        if (skip != null && take != null)
            query = query.Skip(skip.Value).Take(take.Value);

        return await query
            .Select(selector)
            .ToListAsync();
    }

    /// <inheritdoc />
    public async Task<T> AddAsync(T entity)
    {
        Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<T> entry = await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entry.Entity;
    }

    /// <inheritdoc />
    public async Task<T> UpdateAsync(T entity)
    {
        entity.UpdateDT = DateTime.UtcNow;
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    /// <inheritdoc />
    public async Task<bool> DeleteAsync(int id)
    {
        T entity = await GetByIdAsync(id);
        _dbSet.Remove(entity);
        int affected = await _context.SaveChangesAsync();
        return affected > 0;
    }
} 
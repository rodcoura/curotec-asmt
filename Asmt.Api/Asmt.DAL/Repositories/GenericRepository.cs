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
    public async Task<T> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id)
            ?? throw new KeyNotFoundException($"Entity of type {typeof(T).Name} with id {id} was not found.");
    }

    /// <inheritdoc />
    public async Task<IEnumerable<T>> GetByAsync(int skip, int take, Expression<Func<T, bool>>? filter = null)
    {
        IQueryable<T> query = _dbSet;

        if (filter != null)
        {
            query = query.Where(filter);
        }

        return await query
            .Skip(skip)
            .Take(take)
            .ToListAsync();
    }

    /// <inheritdoc />
    public async Task<T> AddAsync(T entity)
    {
        var entry = await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entry.Entity;
    }

    /// <inheritdoc />
    public async Task<T> UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    /// <inheritdoc />
    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id);
        _dbSet.Remove(entity);
        var affected = await _context.SaveChangesAsync();
        return affected > 0;
    }
} 
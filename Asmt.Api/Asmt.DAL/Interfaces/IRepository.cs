using System;
using System.Linq.Expressions;

namespace Asmt.DAL.Interfaces;

/// <summary>
/// Generic repository interface for data access operations.
/// </summary>
/// <typeparam name="T">The entity type that implements IAtom interface.</typeparam>
public interface IRepository<T> where T : IAtom
{
    /// <summary>
    /// Retrieves an entity by its identifier asynchronously.
    /// </summary>
    /// <param name="id">The identifier of the entity to retrieve.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the entity if found.</returns>
    Task<T> GetByIdAsync(int id);

    /// <summary>
    /// Retrieves a filtered collection of entities asynchronously with pagination support.
    /// </summary>
    /// <param name="selector">The selector to apply to the entities.</param>
    /// <param name="filter">Optional. A filter expression to apply to the entities.</param>
    /// <param name="skip">Optional. The number of entities to skip.</param>
    /// <param name="take">Optional. The number of entities to take.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the collection of entities.</returns>  
    Task<IEnumerable<TProjection>> GetByAsync<TProjection>(Expression<Func<T, TProjection>> selector, Expression<Func<T, bool>>? filter = null, int? skip = null, int? take = null)
        where TProjection : class;

    /// <summary>
    /// Adds a new entity to the repository asynchronously.
    /// </summary>
    /// <param name="entity">The entity to add.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the added entity.</returns>
    Task<T> AddAsync(T entity);

    /// <summary>
    /// Updates an existing entity in the repository asynchronously.
    /// </summary>
    /// <param name="entity">The entity to update.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the updated entity.</returns>
    Task<T> UpdateAsync(T entity);

    /// <summary>
    /// Deletes an entity from the repository asynchronously by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the entity to delete.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains true if deletion was successful, false otherwise.</returns>
    Task<bool> DeleteAsync(int id);
}

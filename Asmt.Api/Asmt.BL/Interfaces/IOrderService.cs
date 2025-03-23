using System;
using Asmt.BL.DTOs;

namespace Asmt.BL.Interfaces;

/// <summary>
/// Interface for the order service.
/// </summary>
public interface IOrderService
{
    /// <summary>
    /// Creates a new order.
    /// </summary>
    /// <param name="orderDto">The order to create.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The created order.</returns>
    Task<OrderDto> CreateOrderAsync(OrderDto orderDto, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing order.
    /// </summary>
    /// <param name="orderDto">The order to update.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The updated order.</returns>
    Task<OrderDto> UpdateOrderAsync(OrderDto orderDto, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes an order.
    /// </summary>
    /// <param name="orderId">The ID of the order to delete.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>True if the order was deleted, false otherwise.</returns>
    Task<bool> DeleteOrderAsync(int orderId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets an order by its ID.
    /// </summary>
    /// <param name="orderId">The ID of the order to get.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The order.</returns>
    Task<OrderDto> GetOrderByIdAsync(int orderId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets all orders.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The orders.</returns>
    Task<IEnumerable<OrderDto>> GetAllOrdersAsync(CancellationToken cancellationToken = default);
}

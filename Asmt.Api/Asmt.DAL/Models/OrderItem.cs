using Asmt.DAL.Interfaces;

namespace Asmt.DAL.Models;

/// <summary>
/// Represents an item within an order.
/// </summary>
public class OrderItem : IAtom
{
    /// <summary>
    /// Gets the unique identifier for the order item.
    /// </summary>
    public int Id { get; init; }

    /// <summary>
    /// Gets the UTC timestamp when the order item was created.
    /// </summary>
    public DateTime CreateDT { get; init; } = DateTime.UtcNow;

    /// <summary>
    /// Gets the UTC timestamp when the order item was last updated, if any.
    /// </summary>
    public DateTime? UpdateDT { get; init; }

    /// <summary>
    /// Gets the price of the order item.
    /// </summary>
    public decimal Price { get; init; }

    /// <summary>
    /// Gets the ID of the parent order.
    /// </summary>
    public required int OrderId { get; init; }

    /// <summary>
    /// Gets the associated parent order.
    /// </summary>
    public virtual required Order Order { get; init; }
}

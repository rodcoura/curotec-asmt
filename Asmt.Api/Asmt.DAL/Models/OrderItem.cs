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
    public int Id { get; set; }

    /// <summary>
    /// Gets the UTC timestamp when the order item was created.
    /// </summary>
    public DateTime CreateDT { get; set; }

    /// <summary>
    /// Gets the UTC timestamp when the order item was last updated, if any.
    /// </summary>
    public DateTime? UpdateDT { get; set; }

    /// <summary>
    /// Gets the price of the order item.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Gets the ID of the parent order.
    /// </summary>
    public required int OrderId { get; set; }

    /// <summary>
    /// Gets the associated parent order.
    /// </summary>
    public virtual Order? Order { get; set; }
}

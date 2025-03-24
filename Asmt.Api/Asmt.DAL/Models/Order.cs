using System;
using System.ComponentModel.DataAnnotations.Schema;
using Asmt.DAL.Interfaces;

namespace Asmt.DAL.Models;

/// <summary>
/// Represents the status of an order.
/// </summary>
public enum OrderStatusType : byte
{
    Pending = 0,    // The order is pending.
    Completed = 1,  // The order is completed.
    Cancelled = 2  // The order is cancelled.
}


/// <summary>
/// Represents an order in the system that contains order items and customer information.
/// </summary>
public class Order : IAtom
{
    /// <summary>
    /// Gets the unique identifier for the order.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets the price of the order before tax is applied.
    /// </summary>
    public decimal? PricePreTax { get; set; }

    /// <summary>
    /// Gets the tax amount for the order.
    /// </summary>
    public decimal? Tax { get; set; }

    /// <summary>
    /// Gets the total price of the order including tax.
    /// </summary>
    public decimal? Price => PricePreTax + Tax / 100;

    /// <summary>
    /// Gets the status of the order.
    /// </summary>
    public OrderStatusType Status { get; set; }

    /// <summary>
    /// Gets the date and time when the order was created.
    /// </summary>
    public DateTime CreateDT { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets the date and time when the order was last updated.
    /// </summary>
    public DateTime? UpdateDT { get; set; }

    /// <summary>
    /// Gets the collection of items included in this order.
    /// </summary>
    public List<OrderItem> OrderItems { get; set; } = new();

    /// <summary>
    /// Gets the identifier of the customer who placed the order.
    /// </summary>
    public required int CustomerId { get; set; }

    /// <summary>
    /// Gets the customer who placed the order.
    /// </summary>
    public virtual Customer? Customer { get; set; }
}

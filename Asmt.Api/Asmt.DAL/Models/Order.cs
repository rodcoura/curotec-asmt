using System;
using System.ComponentModel.DataAnnotations.Schema;
using Asmt.DAL.Interfaces;

namespace Asmt.DAL.Models;

/// <summary>
/// Represents an order in the system that contains order items and customer information.
/// </summary>
public class Order : IAtom
{
    /// <summary>
    /// Gets the unique identifier for the order.
    /// </summary>
    public int Id { get; init; }

    /// <summary>
    /// Gets the price of the order before tax is applied.
    /// </summary>
    public decimal? PricePreTax { get; init; }

    /// <summary>
    /// Gets the tax amount for the order.
    /// </summary>
    public decimal? Tax { get; init; }

    /// <summary>
    /// Gets the total price of the order including tax.
    /// </summary>
    public decimal? Price => PricePreTax + Tax;

    /// <summary>
    /// Gets the date and time when the order was created.
    /// </summary>
    public DateTime CreateDT { get; init; } = DateTime.UtcNow;

    /// <summary>
    /// Gets the date and time when the order was last updated.
    /// </summary>
    public DateTime? UpdateDT { get; init; }

    /// <summary>
    /// Gets the collection of items included in this order.
    /// </summary>
    public List<OrderItem> OrderItems { get; init; } = new();

    /// <summary>
    /// Gets the identifier of the customer who placed the order.
    /// </summary>
    public required int CustomerId { get; init; }

    /// <summary>
    /// Gets the customer who placed the order.
    /// </summary>
    public virtual required Customer Customer { get; init; }
}

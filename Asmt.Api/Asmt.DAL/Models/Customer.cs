using Asmt.DAL.Interfaces;

namespace Asmt.DAL.Models;

/// <summary>
/// Represents a customer entity in the system.
/// </summary>
public class Customer : IAtom
{
    /// <summary>
    /// Gets the unique identifier for the customer.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets the name of the customer.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Gets the UTC timestamp when the customer record was created.
    /// </summary>
    public DateTime CreateDT { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets the UTC timestamp when the customer record was last updated.
    /// Can be null if the record has never been updated.
    /// </summary>
    public DateTime? UpdateDT { get; set; }

    /// <summary>
    /// Gets the list of orders associated with this customer.
    /// </summary>
    public List<Order> Orders { get; set; } = new();
}
namespace Asmt.BL.DTOs;

/// <summary>
/// DTO representing an order item for API operations.
/// </summary>
public class OrderItemDto
{
    /// <summary>
    /// The unique identifier for the order item.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The price of the order item.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// The ID of the parent order.
    /// </summary>
    public int OrderId { get; set; }
}
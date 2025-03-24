using Asmt.BL.Validators;
using Asmt.DAL.Models;
using FluentValidation.Results;

namespace Asmt.BL.DTOs;

/// <summary>
/// DTO representing an order for API operations.
/// </summary>
public class OrderDto
{
    /// <summary>
    /// The unique identifier for the order.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The price of the order before tax.
    /// </summary>
    public decimal? PricePreTax { get; set; }

    /// <summary>
    /// The tax amount for the order.
    /// </summary>
    public decimal? Tax { get; set; }

    /// <summary>
    /// The total price of the order including tax.
    /// </summary>
    public decimal? TotalPrice => PricePreTax + Tax / 100;

    /// <summary>
    /// The status of the order.
    /// </summary>
    public OrderStatusType Status { get; set; }

    /// <summary>
    /// The identifier of the customer who placed the order.
    /// </summary>
    public int CustomerId { get; set; }

    /// <summary>
    /// The Customer Name
    /// </summary>
    public string? CustomerName { get; set; }

    /// <summary>
    /// The collection of items included in this order.
    /// </summary>
    public List<OrderItemDto> OrderItems { get; set; } = new();

    /// <summary>
    /// Validates the order DTO.
    /// </summary>
    /// <returns>The validation result.</returns>
    public Task<ValidationResult> ValidateAsync(CancellationToken cancellationToken = default)
    {
        OrderDtoValidator validator = new();
        return validator.ValidateAsync(this, cancellationToken);
    }
}
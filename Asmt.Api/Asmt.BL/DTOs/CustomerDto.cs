using Asmt.BL.Validators;
using FluentValidation.Results;

namespace Asmt.BL.DTOs;

/// <summary>
/// DTO representing a customer for API operations.
/// </summary>
public class CustomerDto
{
    /// <summary>
    /// The unique identifier for the customer.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The name of the customer.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// The UTC timestamp when the customer record was last updated.
    /// </summary>
    public DateTime? UpdateDT { get; set; }

    /// <summary>
    /// Validates the customer DTO.
    /// </summary>
    /// <returns>The validation result.</returns>
    public Task<ValidationResult> ValidateAsync(CancellationToken cancellationToken = default)
    {
        CustomerDtoValidator validator = new();
        return validator.ValidateAsync(this, cancellationToken);
    }
}
using FluentValidation;
using Asmt.BL.DTOs;
using Asmt.BL.DTOs.Validators;

namespace Asmt.BL.Validators;

public class OrderDtoValidator : AbstractValidator<OrderDto>
{
    public OrderDtoValidator()
    {
        RuleFor(x => x.CustomerId)
            .GreaterThan(0)
            .WithMessage("Customer ID must be greater than 0");

        RuleFor(x => x.PricePreTax)
            .NotNull()
            .WithMessage("Price before tax is required")
            .GreaterThanOrEqualTo(0)
            .WithMessage("Price before tax must be greater than or equal to 0");

        RuleFor(x => x.Tax)
            .NotNull()
            .WithMessage("Tax amount is required")
            .GreaterThanOrEqualTo(0)
            .WithMessage("Tax amount must be greater than or equal to 0");

        RuleForEach(x => x.OrderItems)
            .SetValidator(new OrderItemDtoValidator())
            .When(x => x.OrderItems?.Count > 0);
    }
} 
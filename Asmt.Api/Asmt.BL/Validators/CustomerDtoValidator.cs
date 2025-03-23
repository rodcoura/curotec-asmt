using FluentValidation;
using Asmt.BL.DTOs;

namespace Asmt.BL.Validators;

public class CustomerDtoValidator : AbstractValidator<CustomerDto>
{
    public CustomerDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Customer name is required")
            .MaximumLength(100)
            .WithMessage("Customer name cannot exceed 100 characters");
    }
} 
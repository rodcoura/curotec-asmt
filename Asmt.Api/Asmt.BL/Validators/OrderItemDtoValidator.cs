using FluentValidation;

namespace Asmt.BL.DTOs.Validators
{
    public class OrderItemDtoValidator : AbstractValidator<OrderItemDto>
    {
        public OrderItemDtoValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x.Price)
                .GreaterThan(0)
                .WithMessage("Price must be greater than zero");

            RuleFor(x => x.OrderId)
                .GreaterThan(0)
                .WithMessage("OrderId must be greater than zero");
        }
    }
}
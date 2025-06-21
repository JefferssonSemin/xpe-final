using Delivery.Comunication.RequestModel.Product;
using Delivery.Exception;
using FluentValidation;

namespace Delivery.Application.UseCases.Product.Register;

public class RegisterProductValidator: AbstractValidator<RequestProductJson>
{
    public RegisterProductValidator()
    {
        RuleFor(product => product.Name).NotEmpty().WithMessage(ResourceErrorMessages.NAME_REQUIRED);
        RuleFor(product => product.Cod).NotEmpty().WithMessage(ResourceErrorMessages.CODE_REQUIRED);
        RuleFor(product => product.Description).NotEmpty().WithMessage(ResourceErrorMessages.DESCRIPTION_REQUIRED);
        RuleFor(product => product.UnitMeasure).NotEmpty().MaximumLength(5).WithMessage(ResourceErrorMessages.UNIT_MEASURE_REQUIRED);
    }
}
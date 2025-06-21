using Delivery.Comunication.RequestModel.Deliveryman;
using Delivery.Exception;
using FluentValidation;

namespace Delivery.Application.UseCases.Deliveryman.Register;

public class RegisterDeliveryValidator : AbstractValidator<RequestRegisterDeliverymanJson>
{
    public RegisterDeliveryValidator()
    {
        RuleFor(deliveryman => deliveryman.Name).NotEmpty().WithMessage(ResourceErrorMessages.NAME_REQUIRED);
        RuleFor(deliveryman => deliveryman.Email).NotEmpty().EmailAddress()
            .WithMessage(ResourceErrorMessages.EMAIL_REQUIRED);
        RuleFor(deliveryman => deliveryman.Cpf).NotEmpty().WithMessage(ResourceErrorMessages.CPF_REQUIRED);
    }
}
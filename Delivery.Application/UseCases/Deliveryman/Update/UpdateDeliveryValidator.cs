using Delivery.Comunication.RequestModel.Deliveryman;
using Delivery.Exception;
using FluentValidation;

namespace Delivery.Application.UseCases.Deliveryman.Update;

public class UpdateDeliveryValidator : AbstractValidator<RequestUpdateDeliverymanJson>
{
    public UpdateDeliveryValidator()
    {
        RuleFor(deliveryman => deliveryman.Name).NotEmpty().WithMessage(ResourceErrorMessages.NAME_REQUIRED);
        RuleFor(deliveryman => deliveryman.Email).NotEmpty().EmailAddress()
            .WithMessage(ResourceErrorMessages.EMAIL_REQUIRED);
        RuleFor(deliveryman => deliveryman.DateOfBirth).NotEmpty()
            .Must(date => date <= DateOnly.FromDateTime(DateTime.Now))
            .WithMessage(ResourceErrorMessages.INVALID_DATE_OF_BIRTH);
    }
}
using Delivery.Comunication.RequestModel.User;
using Delivery.Exception;
using FluentValidation;

namespace Delivery.Application.UseCases.User.Update;

public class UpdateUserValidator: AbstractValidator<RequestUpdateUserJson>
{
    public UpdateUserValidator()
    {
        RuleFor(user => user.UserName).NotEmpty().WithMessage(ResourceErrorMessages.USERNAME_REQUIRED);
        RuleFor(user => user.Role).NotEmpty().WithMessage(ResourceErrorMessages.ROLE_REQUIRED);
    }
}
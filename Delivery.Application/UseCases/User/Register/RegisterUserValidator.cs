using Delivery.Comunication.RequestModel.User;
using Delivery.Exception;
using FluentValidation;

namespace Delivery.Application.UseCases.User.Register;

public class RegisterUserValidator : AbstractValidator<RequestRegisterUserJson>
{
    public RegisterUserValidator()
    {
        RuleFor(user => user.UserName).NotEmpty().WithMessage(ResourceErrorMessages.USERNAME_REQUIRED);
        RuleFor(user => user.Password).SetValidator(new PasswordValidator<RequestRegisterUserJson>());
        RuleFor(user => user.Role).NotEmpty().WithMessage(ResourceErrorMessages.ROLE_REQUIRED);
    }
}
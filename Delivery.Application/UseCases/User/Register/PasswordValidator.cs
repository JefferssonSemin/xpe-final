using Delivery.Exception;
using FluentValidation;
using FluentValidation.Validators;

namespace Delivery.Application.UseCases.User.Register;

public class PasswordValidator<T> : PropertyValidator<T, string>
{
    public override string Name => "PasswordValidator";
    
    public override bool IsValid(ValidationContext<T> context, string password)
    {
        if (!string.IsNullOrWhiteSpace(password)) return true;
        context.MessageFormatter.AppendArgument(ResourceErrorMessages.INVALID_PASSWORD, password);
        
        if (password.Length > 5) return true;
        context.MessageFormatter.AppendArgument(ResourceErrorMessages.INVALID_PASSWORD, password);

        return false;
    }
}
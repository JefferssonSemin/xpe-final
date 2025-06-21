using CommonTestUtilities.Requests;
using Delivery.Application.UseCases.Deliveryman.Register;
using Delivery.Exception;
using FluentAssertions;

namespace Validator.Tests.Deliverymen.Register;

public class RegisterDeliverymanValidatorTests
{
    [Fact]
    public void Success()
    {
        // Arrange
        var validator = new RegisterDeliveryValidator();
        var request = RequestRegisterDeliverymanBuilder.Build();

        // Act
        var result = validator.Validate(request);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Error_Name_Empty()
    {
        // Arrange
        var validator = new RegisterDeliveryValidator();
        var request = RequestRegisterDeliverymanBuilder.Build();
        request.Name = string.Empty;

        // Act
        var result = validator.Validate(request);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And
            .Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.NAME_REQUIRED));
    }

    [Fact]
    public void Error_Cpf_Empty()
    {
        // Arrange
        var validator = new RegisterDeliveryValidator();
        var request = RequestRegisterDeliverymanBuilder.Build();
        request.Cpf = string.Empty;

        // Act
        var result = validator.Validate(request);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And
            .Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.CPF_REQUIRED));
    }

    [Fact]
    public void Error_Email_Valid()
    {
        // Arrange
        var validator = new RegisterDeliveryValidator();
        var request = RequestRegisterDeliverymanBuilder.Build();
        request.Email = "tetsettag";

        // Act
        var result = validator.Validate(request);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And
            .Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.EMAIL_REQUIRED));
    }
}
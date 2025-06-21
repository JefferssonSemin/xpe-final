using Bogus;
using Bogus.Extensions.Brazil;
using Delivery.Comunication.RequestModel.Deliveryman;

namespace CommonTestUtilities.Requests;

public class RequestRegisterDeliverymanBuilder
{
    public static RequestRegisterDeliverymanJson Build()
    {
        return new Faker<RequestRegisterDeliverymanJson>()
            .RuleFor(d => d.Name, faker => faker.Person.Cpf())
            .RuleFor(d => d.Cpf, faker => faker.Person.Cpf())
            .RuleFor(d => d.Email, faker => faker.Person.Email)
            .RuleFor(d => d.DateOfBirth, faker => DateOnly.FromDateTime(faker.Person.DateOfBirth))
            .RuleFor(d => d.CreatedAt, DateTime.Now);
    }
}
using Bogus;
using Delivery.Comunication.RequestModel.User;
using Delivery.Domain.Enums;

namespace CommonTestUtilities.Requests;

public abstract class RequestRegisterUserJsonBuilder
{
    public static RequestRegisterUserJson Build()
    {
        return new Faker<RequestRegisterUserJson>()
            .RuleFor(d => d.UserName, faker => faker.Person.FirstName)
            .RuleFor(d => d.Password, faker => faker.Internet.Password(prefix: "!Aa1"))
            .RuleFor(d => d.Role, faker => faker.PickRandom(Roles.Admin, Roles.Deliveryman));
    }
}
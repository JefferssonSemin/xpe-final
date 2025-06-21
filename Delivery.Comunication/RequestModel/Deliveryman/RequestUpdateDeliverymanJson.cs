namespace Delivery.Comunication.RequestModel.Deliveryman;

public class RequestUpdateDeliverymanJson(DateOnly? dateOfBirth, string name, string email)
{
    public string Name { get; } = name;
    public string Email { get;  } = email;
    public DateOnly? DateOfBirth { get; } = dateOfBirth;
}
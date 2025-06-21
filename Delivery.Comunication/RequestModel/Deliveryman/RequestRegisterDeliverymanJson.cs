namespace Delivery.Comunication.RequestModel.Deliveryman;

public class RequestRegisterDeliverymanJson(DateOnly? dateOfBirth)
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Cpf { get; set; } = string.Empty;
    public DateOnly? DateOfBirth { get;  } = dateOfBirth;
    public DateTime? CreatedAt { get;  } = DateTime.UtcNow;
}
namespace Delivery.Comunication.ResponseModel.Deliveryman;

public class ResponseDeliverymanJson(string cpf)
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string Cpf { get; init; } = cpf;
}
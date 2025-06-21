namespace Delivery.Comunication.ResponseModel.Deliveryman;

public class ResponseRegisterDeliverymanJson(DateTime? createdAt)
{
    public long Id { get; set; }
    public string Name { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public DateTime? CreatedAt { get; } =  createdAt;
}
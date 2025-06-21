namespace Delivery.Comunication.ResponseModel.Deliveryman;

public class ResponseErrorJson(List<string> errors)
{
    public List<string> Errors { get; init; } = errors;
}
namespace Delivery.Exception;

public abstract class DeliverymanException(string message) : SystemException(message)
{
    public abstract int StatusCode { get; }
    public abstract List<string> GetErrors();
}
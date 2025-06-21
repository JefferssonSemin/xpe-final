using System.Net;

namespace Delivery.Exception.ExceptionsBase;

public class NotFoundException(string message) : DeliverymanException(message)
{
    private readonly string _message = message;
    public override int StatusCode => (int)HttpStatusCode.NotFound;

    public override List<string> GetErrors()
    {
        return [_message];
    }
}
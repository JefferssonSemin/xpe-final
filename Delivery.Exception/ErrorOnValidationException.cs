using System.Net;

namespace Delivery.Exception;

public class ErrorOnValidationException(List<string> errors) : DeliverymanException(string.Empty)
{
    public override int StatusCode => (int)HttpStatusCode.BadRequest;

    public override List<string> GetErrors()
    {
        return errors;
    }
}
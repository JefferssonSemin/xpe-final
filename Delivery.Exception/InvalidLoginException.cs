using System.Net;

namespace Delivery.Exception;

public class InvalidLoginException() : DeliverymanException(ResourceErrorMessages.UNHAUTORIZED_ERROR)
{
    public override int StatusCode => (int)HttpStatusCode.Unauthorized;
    public override List<string> GetErrors()
    {
        throw new NotImplementedException();
    }
}
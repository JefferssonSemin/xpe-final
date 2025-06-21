using Delivery.Comunication.ResponseModel.Deliveryman;
using Delivery.Exception;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Delivery.Filters;

public class ExceptionFilter(ILogger<ExceptionFilter> log) : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        switch (context.Exception)
        {
            case InvalidLoginException:
                ThrowUnauthorizedException(context);
                break;
            case DeliverymanException:
                HandleProjectException(context);
                break;
            default:
                ThrowUnknowException(context);
                break;
        }
    }

    private void HandleProjectException(ExceptionContext context)
    {
        var deliverymanException = (DeliverymanException)context.Exception;
        var errorResponse = new ResponseErrorJson(deliverymanException.GetErrors());

             
        log.LogError("Error logado:  {exceptionMessage} --- {innerExceptionMessage}", context.Exception.Message, context.Exception.InnerException?.Message);
        context.HttpContext.Response.StatusCode = deliverymanException.StatusCode;
        context.Result = new ObjectResult(errorResponse);
    }

    private void ThrowUnknowException(ExceptionContext context)
    {
        var errorResponse = new ResponseErrorJson([ResourceErrorMessages.UNKOWN_ERROR]);
        
        log.LogError("Error logado:  {exceptionMessage} --- {innerExceptionMessage}", context.Exception.Message, context.Exception.InnerException?.Message);
        context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Result = new ObjectResult(errorResponse);
    }
    
    private void ThrowUnauthorizedException(ExceptionContext context)
    {
        var errorResponse = new ResponseErrorJson([ResourceErrorMessages.UNHAUTORIZED_ERROR]);

               
        log.LogError("Error logado:  {exceptionMessage} --- {innerExceptionMessage}", context.Exception.Message, context.Exception.InnerException?.Message);
        context.HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
        context.Result = new ObjectResult(errorResponse);
    }
}
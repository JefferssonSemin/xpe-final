using Delivery.Application.UseCases.User.Login;
using Delivery.Comunication.RequestModel.User;
using Delivery.Comunication.ResponseModel.Deliveryman;
using Delivery.Comunication.ResponseModel.User;
using Microsoft.AspNetCore.Mvc;

namespace Delivery.Controller;

[ApiController]
[Route("api/v1/[controller]")]
public class LoginController : ControllerBase   
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseUserJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login([FromBody] RequestLoginJson request, IDoLoginUseCase useCase)
    {
        var result = await useCase.ExecuteAsync(request.Username, request.Password);
        return Ok(result);
    }
}
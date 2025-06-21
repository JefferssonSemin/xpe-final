using Delivery.Application.UseCases.User.GetAll;
using Delivery.Application.UseCases.User.GetById;
using Delivery.Application.UseCases.User.Register;
using Delivery.Application.UseCases.User.Update;
using Delivery.Comunication.RequestModel.User;
using Delivery.Comunication.ResponseModel.Deliveryman;
using Delivery.Comunication.ResponseModel.User;
using Microsoft.AspNetCore.Mvc;

namespace Delivery.Controller;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseUserJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] RequestRegisterUserJson request,
        [FromServices] IRegisterUserUseCase useCase)
    {
        var result = await useCase.ExecuteAsync(request);

        return Created(string.Empty, result);
    }
    
    [HttpGet]
    [ProducesResponseType(typeof(ResponseUserProfileJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetAll([FromServices] IGetAllUserUseCase useCase)
    {
        var result = await useCase.ExecuteAsync();

        return Ok(result);
    }
    
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ResponseUserJson>> GetById(int id, [FromServices]  IGetByIdUserUseCase useCase)
    {
        var user = await useCase.ExecuteAsync(id);

        return Ok(user);
    }
       
    [HttpPut]
    [ProducesResponseType(typeof(ResponseUserProfileJson), StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ResponseUserProfileJson>> Update(RequestUpdateUserJson request, 
        [FromServices] IUpdateUserUseCase useCase)
    {
        var user = await useCase.ExecuteAsync(request);

        return Ok(user);
    }

}
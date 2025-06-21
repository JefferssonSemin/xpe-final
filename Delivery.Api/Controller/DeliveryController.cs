using Delivery.Application.UseCases.Deliveryman.Delete;
using Delivery.Application.UseCases.Deliveryman.GetAll;
using Delivery.Application.UseCases.Deliveryman.GetById;
using Delivery.Application.UseCases.Deliveryman.Register;
using Delivery.Application.UseCases.Deliveryman.Update;
using Delivery.Comunication.RequestModel.Deliveryman;
using Delivery.Comunication.ResponseModel.Deliveryman;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Delivery.Controller;

[ApiController]
[Route("[controller]")]
[Authorize]
public class DeliveryController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisterDeliverymanJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] RequestRegisterDeliverymanJson requestRegister,
        [FromServices] IRegisterDeliverymanUseCase useCase)
    {
        var result = await useCase.ExecuteAsync(requestRegister);

        return Created(string.Empty, result);
    }
    
    [HttpGet]
    [ProducesResponseType(typeof(ResponseDeliverymenJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetAll([FromServices] IGetAllDeliverymanUseCase useCase)
    {
        var result = await useCase.ExecuteAsync();

        if (result.Deliverymen.Count == 0)
            return NoContent();

        return Ok(result);
    }

    [HttpGet]
    [Route("{id:int}")]
    [ProducesResponseType(typeof(ResponseDeliverymanJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetById([FromServices] IGetByIdDeliverymanUseCase useCase, [FromRoute] int id)
    {
        var result = await useCase.ExecuteAsync(id);

        return Ok(result);
    }

    [HttpDelete]
    [Route("{id:int}")]
    [ProducesResponseType(typeof(ResponseDeliverymanJson), StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromServices] IDeleteDeliverymanUseCase useCase, [FromRoute] int id)
    {
        await useCase.ExecuteAsync(id);
        return NoContent();
    }

    [HttpPut]
    [Route("{id:int}")]
    [ProducesResponseType(typeof(ResponseDeliverymanJson), StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update([FromServices] IUpdateDeliverymanUseCase useCase,
        [FromRoute] int id,
        [FromBody] RequestUpdateDeliverymanJson requestUpdate)
    {
        await useCase.ExecuteAsync(id, requestUpdate);

        return NoContent();
    }
}
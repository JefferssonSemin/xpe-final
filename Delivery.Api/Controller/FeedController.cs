using Delivery.Application.UseCases.Feed.GetAll;
using Delivery.Application.UseCases.Feed.GetById;
using Delivery.Application.UseCases.Feed.Like;
using Delivery.Application.UseCases.Feed.Register;
using Delivery.Comunication.RequestModel.Feed;
using Delivery.Comunication.ResponseModel.Deliveryman;
using Delivery.Comunication.ResponseModel.Feed;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Delivery.Controller;

[ApiController]
[Route("[controller]")]
[Authorize]
public class FeedController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseFeedJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] RequestRegisterFeedJson requestRegister,
        [FromServices] IRegisterFeedUseCase useCase)
    {
        var result = await useCase.ExecuteAsync(requestRegister);

        return Created(string.Empty , result);
    }
    
    [HttpGet]
    [Route("{id:int}")]
    [ProducesResponseType(typeof(ResponseFeedJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetById([FromServices] IGetByIdFeedUseCase useCase, [FromRoute] int id)
    {
        var result = await useCase.ExecuteAsync(id);

        return Ok(result);
    }
        
    [HttpGet]
    [ProducesResponseType(typeof(ResponseFeedJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetAll([FromServices] IGetAllFeedUseCase useCase)
    {
        var result = await useCase.ExecuteAsync();

        return Ok(result);
    }
    
    [HttpPatch]
    [Route("{id:int}")]
    [ProducesResponseType(typeof(ResponseFeedJson), StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Like(long id, [FromServices] ILikeFeedUseCase useCase)
    {
        await useCase.ExecuteAsync(id);

        return NoContent();
    }
}
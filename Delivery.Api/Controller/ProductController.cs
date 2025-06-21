using Delivery.Application.UseCases.Product.Register;
using Delivery.Application.UseCases.Product.Search;
using Delivery.Comunication.RequestModel.Product;
using Delivery.Comunication.ResponseModel.Deliveryman;
using Delivery.Comunication.ResponseModel.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Delivery.Controller;

[ApiController]
[Route("[controller]")]
[Authorize]
public class ProductController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType( typeof(ResponseProductJson),StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] RequestProductJson request,
        [FromServices] IRegisterProductUseCase useCase)
    {
        var result = await useCase.ExecuteAsync(request);

        return Created(string.Empty, result);
    }
    
    [HttpGet]
    [ProducesResponseType( typeof(ResponseProductJson),StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Get([FromServices] ISearchProductUseCase useCase, [FromQuery]string query)
    {
        var result = await useCase.ExecuteAsync(query);

        return Ok(result);
    }
}
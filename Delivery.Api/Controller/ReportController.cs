using System.Net.Mime;
using Delivery.Application.UseCases.Deliveryman.Reports.Excel;
using Delivery.Application.UseCases.Deliveryman.Reports.Pdf;
using Delivery.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Delivery.Controller;

[ApiController]
[Route("[controller]")]
[Authorize(Roles = Roles.Admin)]
public class ReportController : ControllerBase
{
    [HttpGet("excel")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetExcel([FromQuery] DateOnly month,
        [FromServices] IGenerateDeliveryReportExcelUseCase useCase)
    {
        var data = await useCase.ExecuteAsync(month);

        if (data.Length > 0)
            return File(data, MediaTypeNames.Application.Octet, "report.xlsx");
        
        return NoContent();
    }

    [HttpGet("pdf")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetPdf([FromQuery] DateOnly month,
        [FromServices] IGenerateDeliveyReportPdfUseCase useCase)
    {
        var data = await useCase.ExecuteAsync(month);

        if (data.Length > 0)
            return File(data, MediaTypeNames.Application.Pdf, "report.pdf");

        return NoContent();
    }
}
namespace Delivery.Application.UseCases.Deliveryman.Reports.Pdf;

public interface IGenerateDeliveyReportPdfUseCase
{
    public Task<byte[]> ExecuteAsync(DateOnly month);
}
namespace Delivery.Application.UseCases.Deliveryman.Reports.Excel;

public interface IGenerateDeliveryReportExcelUseCase
{
    public Task<byte[]> ExecuteAsync(DateOnly month);
}
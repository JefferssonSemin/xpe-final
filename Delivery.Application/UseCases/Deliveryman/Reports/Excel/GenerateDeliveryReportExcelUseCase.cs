using ClosedXML.Excel;
using Delivery.Domain.Reports;
using Delivery.Domain.Repositories.Deliveryman;
using Delivery.Domain.Security.Logged;

namespace Delivery.Application.UseCases.Deliveryman.Reports.Excel;

public class GenerateDeliveryReportExcelUseCase(IDeliverymanReadOnlyRepository repository, ILoggedUser loggedUser)
    : IGenerateDeliveryReportExcelUseCase
{
    public async Task<byte[]> ExecuteAsync(DateOnly month)
    {
        var user = await loggedUser.Get();
        
        var deliverymen = await repository.FiltersByMonthBirth(user, month);

        if (deliverymen.Count == 0)
            return [];

        using var workbook = new XLWorkbook();

        workbook.Author = user.UserName;
        workbook.Style.Font.FontName = "Arial";
        workbook.Style.Font.FontSize = 12;

        month.ToString("Y");
        var worksheet = workbook.Worksheets.Add(month.ToString("Y"));

        InsertHeader(worksheet);

        var raw = 2;
        foreach (var deliveryman in deliverymen)
        {
            worksheet.Cell($"A{raw}").Value = deliveryman.Name;
            worksheet.Cell($"B{raw}").Value = deliveryman.Email;
            worksheet.Cell($"C{raw}").Value = deliveryman.Cpf;

            worksheet.Cell($"D{raw}").Value = deliveryman.DateOfBirth.ToString();
            worksheet.Cell($"D{raw}").Style.DateFormat.Format = deliveryman.DateOfBirth.ToString();

            worksheet.Cell($"E{raw}").Value = deliveryman.CreatedAt;

            raw++;
        }

        worksheet.Columns().AdjustToContents();

        var file = new MemoryStream();
        workbook.SaveAs(file);

        return file.ToArray();
    }

    private static void InsertHeader(IXLWorksheet worksheet)
    {
        worksheet.Cell("A1").Value = ResourceReportGenerationMessages.NAME;
        worksheet.Cell("B1").Value = ResourceReportGenerationMessages.EMAIL;
        worksheet.Cell("C1").Value = ResourceReportGenerationMessages.CPF;
        worksheet.Cell("D1").Value = ResourceReportGenerationMessages.DATE_BIRTH;
        worksheet.Cell("E1").Value = ResourceReportGenerationMessages.CREATE_AT;

        worksheet.Cells("A1:E1").Style.Font.Bold = true;
        worksheet.Cells("A1:E1").Style.Fill.BackgroundColor = XLColor.LightBlue;

        worksheet.Cells("A1:E1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
    }
}
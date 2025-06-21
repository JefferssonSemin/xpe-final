using Delivery.Application.UseCases.Deliveryman.Reports.Pdf.Fonts;
using Delivery.Domain.Reports;
using Delivery.Domain.Repositories.Deliveryman;
using Delivery.Domain.Security.Logged;
using Delivery.Exception;
using Delivery.Exception.ExceptionsBase;
using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
using PdfSharp.Fonts;

namespace Delivery.Application.UseCases.Deliveryman.Reports.Pdf;

public class GenerateDeliveryReportPdfUseCase : IGenerateDeliveyReportPdfUseCase
{
    
    private readonly IDeliverymanReadOnlyRepository _repository;
    private readonly ILoggedUser _loggedUser;
    public GenerateDeliveryReportPdfUseCase(IDeliverymanReadOnlyRepository repository, ILoggedUser loggedUser)
    {
        _repository = repository;
        _loggedUser = loggedUser;
        GlobalFontSettings.FontResolver = new DeliverymanReportFontResolve();
    }
    public async Task<byte[]> ExecuteAsync(DateOnly month)
    {
        var user = await _loggedUser.Get();
        
        var deliverymen = await _repository.FiltersByMonthBirth(user, month);

        if (deliverymen.Count == 0) throw new NotFoundException(ResourceErrorMessages.DELIVERYMAN_NOT_FOUND);

        var document = CreateDocument(user.UserName, month);
        var page = CreatePage(document);

        var paragraph = page.AddParagraph();
        var title = string.Format(ResourceReportGenerationMessages.TITLE_REPORT_DELIVERYMAN, month.ToString("Y"));

        paragraph.AddFormattedText(title, new Font { Name = FontHelper.OpenSansItalic, Size = 15 });
        paragraph.AddLineBreak();

        var totalDeliverymen = deliverymen.Count;
        paragraph.AddFormattedText($"{ResourceReportGenerationMessages.TOTAL_DELIVERYMAN_MONTH} : {totalDeliverymen}",
            new Font { Name = FontHelper.ShareTechRegular, Size = 40 });

        return RenderDocument(document);
    }

    private static Document CreateDocument(string author, DateOnly month)
    {
        var document = new Document
        {
            Info =
            {
                Title = $" Deliveryman Report {ResourceReportGenerationMessages.CREATE_AT} {month: Y}",
                Author = author
            }
        };

        var style = document.Styles["Normal"];
        style!.Font.Name = FontHelper.OpenSansItalic;

        return document;
    }

    private static Section CreatePage(Document document)
    {
        var section = document.AddSection();

        section.PageSetup = document.DefaultPageSetup.Clone();

        section.PageSetup.PageFormat = PageFormat.A4;
        section.PageSetup.LeftMargin = 40;
        section.PageSetup.RightMargin = 40;
        section.PageSetup.TopMargin = 80;
        section.PageSetup.BottomMargin = 80;

        return section;
    }

    private static byte[] RenderDocument(Document document)
    {
        var renderer = new PdfDocumentRenderer
        {
            Document = document
        };

        renderer.RenderDocument();

        using var stream = new MemoryStream();
        renderer.PdfDocument.Save(stream);

        return stream.ToArray();
    }
}
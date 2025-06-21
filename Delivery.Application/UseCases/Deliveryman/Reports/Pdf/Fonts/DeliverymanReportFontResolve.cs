using System.Reflection;
using PdfSharp.Fonts;

namespace Delivery.Application.UseCases.Deliveryman.Reports.Pdf.Fonts;

public class DeliverymanReportFontResolve: IFontResolver
{
    public FontResolverInfo ResolveTypeface(string familyName, bool bold, bool italic)
    {
        return new FontResolverInfo(familyName);
    }

    public byte[] GetFont(string faceName)
    {
        var stream = ReadFontFile(faceName) ?? ReadFontFile(FontHelper.DefaultFont);

        var length = stream!.Length;
        var buffer = new byte[length];

        stream.ReadExactly(buffer, 0, (int)length);
        return buffer;
    }

    private static Stream? ReadFontFile(string faceName)
    {
        var assembly = Assembly.GetExecutingAssembly();

        return assembly.GetManifestResourceStream(
            $"Delivery.Application.UseCases.Deliveryman.Reports.Pdf.Fonts.{faceName}.ttf");
    }
}
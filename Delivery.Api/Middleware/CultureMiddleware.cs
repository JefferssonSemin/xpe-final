using System.Globalization;

namespace Delivery.Middleware;

public class CultureMiddleware(RequestDelegate next)
{
    public async Task Invoke(HttpContext context)
    {
        var culture = context.Request.Headers.AcceptLanguage.FirstOrDefault();

        //to do better
        var cultureInfo = new CultureInfo("en");
        var enUs = culture?.Split(',')
            .FirstOrDefault(x => x.Contains("en-US"))
            ?.Split(';')[0];


        if (!string.IsNullOrWhiteSpace(enUs))
            cultureInfo = new CultureInfo(enUs);

        CultureInfo.CurrentCulture = cultureInfo;
        CultureInfo.CurrentUICulture = cultureInfo;

        await next(context);
    }
}
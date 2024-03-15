using Microsoft.AspNetCore.Mvc.Rendering;

namespace SibSIU.Identity.Infrastructure;

public static class HtmlHelperExtension
{
    public static string ActiveClass(this IHtmlHelper htmlHelper, string route)
    {
        var routeData = htmlHelper.ViewContext.HttpContext.Request.Path;
        bool isCorrect = routeData.HasValue && routeData.Value!.Contains(route);
        return isCorrect ? "active" : "";
    }
}

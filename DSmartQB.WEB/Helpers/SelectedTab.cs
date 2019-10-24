using System.Web.Mvc;

namespace DSmartQB.WEB.Helpers
{
    public static class SelectedTab
    {
        public static string IsActive(this HtmlHelper html, string control, string action)
        {
            var routeData = html.ViewContext.RouteData;

            var routeAction = (string)routeData.Values["action"];
            var routeControl = (string)routeData.Values["controller"];
            return control.Equals(routeControl) && action.Equals(routeAction) ? "active" : "nav-link";
        }
    }
}
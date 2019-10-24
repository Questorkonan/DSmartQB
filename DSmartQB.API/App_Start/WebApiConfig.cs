using System.Web.Http;
using System.Web.Http.Cors;

namespace DSmartQB.API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                            "DefaultApi",
                            "api/{action}/{id}",
                            new { id = RouteParameter.Optional }
                        );
            GlobalConfiguration.Configuration.Formatters.XmlFormatter.SupportedMediaTypes.Clear();

            EnableCorsAttribute cors = new EnableCorsAttribute("*", "*", "*");
            //config.EnableCors(cors);

        }
    }
}

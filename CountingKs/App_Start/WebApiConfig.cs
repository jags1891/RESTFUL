using Newtonsoft.Json.Serialization;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;

namespace CountingKs
{
    public static class WebApiConfig
  {
    public static void Register(HttpConfiguration config)
    {

     config.Routes.MapHttpRoute(
          name: "Food",
          routeTemplate: "api/nutrition/foods/{foodid}",
          defaults: new {controller="foods", foodid = RouteParameter.Optional }
      );

    config.Routes.MapHttpRoute(
           name: "Measures",
           routeTemplate: "api/nutrition/foods/{foodid}/measures/{id}",
           defaults: new { controller = "foods", id = RouteParameter.Optional }
     );

            // Uncomment the following line of code to enable query support for actions with an IQueryable or IQueryable<T> return type.
            // To avoid processing unexpected or malicious queries, use the validation settings on QueryableAttribute to validate incoming queries.
            // For more information, visit http://go.microsoft.com/fwlink/?LinkId=279712.
            //config.EnableQuerySupport();
            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().FirstOrDefault();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

    }
  }
}
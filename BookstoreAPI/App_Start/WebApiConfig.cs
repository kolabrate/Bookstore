using Elmah.Contrib.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;

namespace BookstoreAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            //Config CORS
            config.EnableCors();

            //Enable Elmah
            config.Services.Add(typeof(IExceptionLogger), new ElmahExceptionLogger());

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );


            //// catch all route mapped to ErrorController so 404 errors
            //// can be logged in elmah
            //config.Routes.MapHttpRoute(
            //    name: "NotFound",
            //    routeTemplate: "{*path}",
            //    defaults: new { controller = "Error", action = "NotFound"}
            //);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Assignment5
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Attention 5 - Adding the service layer for HTTP options to the request processing pipeline
            config.MessageHandlers.Add(new ServiceLayer.HandleHttpOptions());

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { controller = "Root", id = RouteParameter.Optional }
            );
        }
    }
}

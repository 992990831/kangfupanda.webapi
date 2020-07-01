using kangfupanda.webapi.Filter;
using kangfupanda.webapi.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace kangfupanda.webapi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //Web API filters
            config.Filters.Add(new MyActionTimeFilter());
            config.Filters.Add(new MyExceptionFilter());

            // Web API configuration and services            

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );


            GlobalConfiguration.Configuration.MessageHandlers.Add(new WebAPIHandler());
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using Unity.AspNet.WebApi;

namespace SignInProvider
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            var cors = new EnableCorsAttribute(origins: "*", headers: "*", methods: "*");
            config.EnableCors();

            //config.DependencyResolver = new UnityHierarchicalDependencyResolver(UnityConfig.Container);

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Ninject;
using Ninject.Web.Common;
using TaskListRefactoring.Infrastructure;

namespace TaskListRefactoring
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            IKernel k = new StandardKernel();
            config.DependencyResolver = new NinjectDependencyResolver(k);
        }
    }
}

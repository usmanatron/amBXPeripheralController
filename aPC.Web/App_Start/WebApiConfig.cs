﻿using System.Web.Http;

namespace aPC.Web
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
          routeTemplate: "api/{controller}/{name}",
          defaults: new { name = RouteParameter.Optional }
      );
    }
  }
}
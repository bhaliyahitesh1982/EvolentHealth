using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Newtonsoft.Json.Serialization;
using Owin;
using System.Web;


namespace EvolentHealth
{
    public class Startup
    {
        /// <summary>
        /// It will creates an OWIN pipeline for hosting Web API, it  also configures the routing
        /// </summary>
        /// <param name="app"></param>
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //Need register Filter here to exeute based on Attribute set on the Controller or class
            config.Filters.Add(new BasicAuthenticationAttribute());

            //Set the response type in the for of JASON
            config.Formatters.Remove(config.Formatters.XmlFormatter);
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            config.Formatters.JsonFormatter.SerializerSettings.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Utc;
            app.UseWebApi(config);
        }
    }
}
using FluentValidation.WebApi;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Newtonsoft.Json;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

/// This assembly attribute directs Microsoft.Owin to use the Startup class
/// defined in this file as the start of our application
[assembly: OwinStartup(typeof(ZigForum.Startup))]

namespace ZigForum
{
    /// <summary>
    /// Startup class used by OWIN implementations to run the Web application
    /// </summary>
    public partial class Startup
    {
        /// <summary>
        /// Used to create an instance of the Web application 
        /// </summary>
        /// <param name="app">Parameter supplied by OWIN implementation which our configuration is connected to</param>
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();
            // Handles registration of the Web API's routes
            WebApiConfig.Register(config);
            // Enables us to call the Web API from domains other than the ones the API responds to
            app.UseCors(CorsOptions.AllowAll);
            // Wire-in the authentication middleware
            ConfigureAuth(app);
            // Add the Web API framework to the app's pipeline
            app.UseWebApi(config);
            // Sets FluentValidation as the default model validator
            FluentValidationModelValidatorProvider.Configure(config);
            // During serialization null properties will be ignored and not added to the final JSON
            config.Formatters.JsonFormatter.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
        }
    }
}

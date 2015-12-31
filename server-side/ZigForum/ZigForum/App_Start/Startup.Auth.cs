using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZigForum.Models;
using ZigForum.Providers;

namespace ZigForum
{
    /// <summary>
    /// Partial of the class used to start the web application
    /// </summary>
    public partial class Startup
    {
        /// <summary>
        /// Options for the authorization system to use when authenticating users
        /// </summary>
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }
        /// <summary>
        /// Factory function that returns a new instance of the UserManager
        /// </summary>
        public static Func<UserManager<User>> UserManagerFactory { get; set; }

        /// <summary>
        /// Static constructor to initialize values on application runtime
        /// </summary>
        static Startup()
        {
            // The "service" (our application) certifying a user's authentication status
            String PublicClientId = "self";

            // Sets the UserManagerFactory to an anonymous function that returns a new
            // instance of UserManager<User>. This factory can be called from
            // anywhere in the application as Startup.UserManagerFactory() to get a properly
            // configured instance of the UserManager
            UserManagerFactory = () => new UserManager<User>(new UserStore<User>(new ZigForumContext()));
            // Options which the authentication system will use
            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                // Point at which the Bearer token middleware will be mounted
                TokenEndpointPath = new PathString("/token"),
                // An implementation of the OAuthAuthorizationServerProvider which the middleware
                // will use for determining whether a user should be authenticated or not
                Provider = new ApplicationOAuthProvider(PublicClientId, UserManagerFactory),
                // How long a bearer token should be valid for
                AccessTokenExpireTimeSpan = TimeSpan.FromHours(24),
                // Allows authentication over HTTP instead of forcing HTTPS
                AllowInsecureHttp = true
            };
        }

        /// <summary>
        /// Configures the application to use the OAuthBearerToken middleware
        /// </summary>
        /// <param name="app">The application to mount the middleware on</param>
        public void ConfigureAuth(IAppBuilder app)
        {
            // Mounts the middleware on the provided app with the options configured
            // above
            app.UseOAuthBearerTokens(OAuthOptions);
        }
    }
}

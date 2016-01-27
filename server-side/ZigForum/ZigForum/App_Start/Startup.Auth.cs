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
    public partial class Startup
    {
        static Startup()
        {
            PublicClientId = "web";

            UserManagerFactory = () => new ApplicationUserManager(new UserStore<ApplicationUser>(new ApplicationDbContext()));

            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/token"),
                Provider = new ApplicationOAuthProvider(PublicClientId, UserManagerFactory),
                AccessTokenExpireTimeSpan = TimeSpan.FromHours(24),
                AllowInsecureHttp = true
            };

            /*
                Example of request to retrieve the token:

                POST /token HTTP/1.1
                Host: localhost:12345
                Content-Type: application/x-www-form-urlencoded
                Cache-Control: no-cache
                Postman-Token: 8b29040e-b84d-4033-ba9e-758d43ed1e2d

                grant_type=password&username=admin&password=password
             */
        }

        public static string PublicClientId { get; private set; }

        public static Func<ApplicationUserManager> UserManagerFactory { get; set; }

        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

        public void ConfigureAuth(IAppBuilder app)
        {
            app.UseOAuthBearerTokens(OAuthOptions);
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);
        }
    }
}

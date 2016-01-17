using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using ZigForum.Models;
using ZigForum.Models.Common;

namespace ZigForum.Controllers.Common
{
    public class ZigForumApiController : ApiController
    {
        private ApplicationDbContext _db;

        protected ApplicationDbContext Db { get { return _db != null ? _db : HttpContext.Current.GetOwinContext().Get<ApplicationDbContext>(); } }
        protected ApplicationUserManager UserManager { get { return HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>(); } }

        public ZigForumApiController()
        {
        }

        public ZigForumApiController(ApplicationDbContext db) : this()
        {
            _db = db;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                Db.Dispose();

            base.Dispose(disposing);
        }
    }
}

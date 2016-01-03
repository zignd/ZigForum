using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using ZigForum.Models;
using ZigForum.Models.Common;

namespace ZigForum.Controllers.Common
{
    public class ZigForumApiController : ApiController
    {
        protected ZigForumContext Db { get; } = new ZigForumContext();
        protected UserManager<User> UserManager { get; set; }

        public ZigForumApiController()
        {
            UserManager = new UserManager<User>(new UserStore<User>(new ZigForumContext()));
        }

        public ZigForumApiController(ZigForumContext db) : this()
        {
            Db = db;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                Db.Dispose();

            base.Dispose(disposing);
        }
    }
}

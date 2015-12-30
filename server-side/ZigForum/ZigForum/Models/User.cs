using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZigForum.Models
{
    public class User : IdentityUser
    {
        public DateTime Created { get; set; }
    }
}

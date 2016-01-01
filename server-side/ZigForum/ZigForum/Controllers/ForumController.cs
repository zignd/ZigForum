using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using ZigForum.Models;
using ZigForum.Models.ViewModels;

namespace ZigForum.Controllers
{
    [RoutePrefix("api/forum")]
    public class ForumController : ApiController
    {
        public async Task<IHttpActionResult> Get()
        {
            using (var db = new ZigForumContext())
            {
                var viewModel = await (from f in db.Forums
                                       select new ForumGetViewModel
                                       {
                                           Id = f.Id,
                                           Name = f.Name,
                                           Created = f.Created,
                                           Parent = f.Parent
                                       }).ToArrayAsync();

                return Ok(viewModel);
            }
        }

        public async Task<IHttpActionResult> Get(int id)
        {
            using (var db = new ZigForumContext())
            {
                var forum = await db.Forums.FindAsync(id);

                if (forum == null)
                    return NotFound();

                var viewModel = new ForumGetViewModel
                {
                    Id = forum.Id,
                    Name = forum.Name,
                    Created = forum.Created,
                    Parent = forum.Parent
                };

                return Ok(viewModel);
            }
        }

        public async Task<IHttpActionResult> Post([FromBody]ForumPostViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            using (var db = new ZigForumContext())
            {
                var forum = new Forum
                {
                    ParentId = viewModel.ParentId,
                    Name = viewModel.Name,
                    Created = DateTime.Now
                };

                var storedForum = db.Forums.Add(forum);
                await db.SaveChangesAsync();

                return Ok(storedForum.Id);
            }
        }

        public async Task<IHttpActionResult> Put(int id, [FromBody]ForumPutViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            using (var db = new ZigForumContext())
            {
                var forum = await db.Forums.FindAsync(id);

                if (forum == null)
                    return NotFound();
                
                forum.ParentId = viewModel.ParentId;
                forum.Name = viewModel.Name;

                db.Forums.Attach(forum);

                var entry = db.Entry<Forum>(forum);
                entry.State = EntityState.Modified;

                await db.SaveChangesAsync();

                return Ok();
            }
        }

        public async Task<IHttpActionResult> Delete(int id)
        {
            using (var db = new ZigForumContext())
            {
                var forum = await db.Forums.FindAsync(id);

                if (forum == null)
                    return NotFound();

                db.Forums.Remove(forum);

                await db.SaveChangesAsync();

                return Ok();
            }
        }
    }
}

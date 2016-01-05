using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using ZigForum.Controllers.Common;
using ZigForum.Models;
using ZigForum.Models.ViewModels;

namespace ZigForum.Controllers
{
    [Authorize(Roles = "Administrator")]
    [RoutePrefix("api/forums")]
    public class ForumsController : ZigForumApiController
    {
        public ForumsController() : base() { }

        public ForumsController(ZigForumContext db) : base(db) { }

        [AllowAnonymous]
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> GetAllForums()
        {
            var viewModel = await (from f in Db.Forums
                                   select new ForumGetViewModel
                                   {
                                       Id = f.Id,
                                       Name = f.Name,
                                       Created = f.Created,
                                       Parent = f.Parent
                                   }).ToArrayAsync();

            return Ok(viewModel);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> GetForumById(int id)
        {
            var forum = await Db.Forums.FindAsync(id);

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

        [HttpPost]
        [Route("create")]
        public async Task<IHttpActionResult> CreateNewForum([FromBody]ForumPostViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var forum = new Forum
            {
                ParentId = viewModel.ParentId,
                Name = viewModel.Name,
                Created = DateTime.Now
            };

            var storedForum = Db.Forums.Add(forum);
            await Db.SaveChangesAsync();

            return Ok(storedForum.Id);
        }

        [HttpPut]
        [Route("{id}/edit")]
        public async Task<IHttpActionResult> EditForum(int id, [FromBody]ForumPutViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var forum = await Db.Forums.FindAsync(id);

            if (forum == null)
                return NotFound();

            forum.ParentId = viewModel.ParentId;
            forum.Name = viewModel.Name;

            Db.Forums.Attach(forum);

            var entry = Db.Entry<Forum>(forum);
            entry.State = EntityState.Modified;

            await Db.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete]
        [Route("{id:int}/delete")]
        public async Task<IHttpActionResult> DeleteForum(int id)
        {
            var forum = await Db.Forums.FindAsync(id);

            if (forum == null)
                return NotFound();

            Db.Forums.Remove(forum);

            await Db.SaveChangesAsync();

            return Ok();
        }
    }
}

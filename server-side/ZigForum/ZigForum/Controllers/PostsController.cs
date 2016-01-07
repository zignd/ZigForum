using Microsoft.AspNet.Identity;
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
    [Authorize]
    [RoutePrefix("api/posts")]
    public class PostsController : ZigForumApiController
    {
        /*public PostsController() : base() { }

        public PostsController(ZigForumContext db) : base(db) { }

        [AllowAnonymous]
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> GetAllPosts()
        {
            var viewModel = await (from f in Db.Posts
                                   select new PostGetViewModel
                                   {
                                       Id = f.Id,
                                       Title = f.Title,
                                       Body = f.Body,
                                       IsLocked = f.IsLocked,
                                       LockedReason = f.LockedReason,
                                       IsDeleted = f.IsDeleted,
                                       Updated = f.Updated,
                                       Created = f.Created,
                                       User = f.User,
                                       Forum = f.Forum
                                   }).ToArrayAsync();

            return Ok(viewModel);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> GetForumById(int id)
        {
            var post = await Db.Posts.FindAsync(id);

            if (post == null)
                return NotFound();

            var viewModel = new PostGetViewModel
            {
                Id = post.Id,
                Title = post.Title,
                Body = post.Body,
                IsLocked = post.IsLocked,
                LockedReason = post.LockedReason,
                IsDeleted = post.IsDeleted,
                Updated = post.Updated,
                Created = post.Created,
                User = post.User,
                Forum = post.Forum
            };

            return Ok(viewModel);
        }

        [HttpPost]
        [Route("create")]
        public async Task<IHttpActionResult> CreateNewPost([FromBody]PostPostViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var post = new Post
            {
                UserId = User.Identity.GetUserId(),
                ForumId = viewModel.ForumId,
                Title = viewModel.Title,
                Body = viewModel.Body,
                Created = DateTime.Now
            };

            var storedPost = Db.Posts.Add(post);
            await Db.SaveChangesAsync();

            return Ok(storedPost.Id);
        }

        [HttpPut]
        [Route("{id}/edit")]
        public async Task<IHttpActionResult> EditForum([FromBody]PostPutViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var post = await Db.Posts.FindAsync(viewModel.Id);

            if (post == null)
                return NotFound();

            post.UserId = User.Identity.GetUserId();
            post.ForumId = viewModel.ForumId;
            post.Title = viewModel.Title;
            post.Body = viewModel.Body;
            post.IsLocked = viewModel.IsLocked;
            post.LockedReason = viewModel.LockedReason;
            post.IsDeleted = viewModel.IsDeleted;
            post.Updated = DateTime.Now;

            Db.Posts.Attach(post);

            Db.MarkAsModified(post);

            await Db.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete]
        [Route("{id:int}/delete")]
        public async Task<IHttpActionResult> DeletePost(int id)
        {
            var post = await Db.Posts.FindAsync(id);

            if (post == null)
                return NotFound();

            Db.Posts.Remove(post);

            await Db.SaveChangesAsync();

            return Ok();
        }*/
    }
}

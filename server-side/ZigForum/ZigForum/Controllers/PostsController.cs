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
using ZigForum.Models.DTOs;
using ZigForum.Models.Validators;

namespace ZigForum.Controllers
{
    [Authorize]
    [RoutePrefix("api/posts")]
    public class PostsController : ZigForumApiController
    {
        public PostsController() : base() { }

        public PostsController(ApplicationDbContext db) : base(db) { }

        [AllowAnonymous]
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> GetById(int id)
        {
            var post = await Db.Posts.FindAsync(id);

            if (post == null)
                return NotFound();

            var data = new PostDTO
            {
                Id = post.Id,
                Title = post.Title,
                Body = post.Body,
                IsLocked = post.IsLocked,
                LockedReason = post.LockedReason,
                Updated = post.Updated,
                Created = post.Created,
                User = new UserDTO
                {
                    UserName = post.User.UserName
                },
                Forum = new ForumDTO
                {
                    Id = post.Forum.Id,
                    Name = post.Forum.Name
                }
            };

            return Ok(data);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("forumid/{id:int}")]
        public async Task<IHttpActionResult> GetByForumId(int id, int page = 1, int pageSize = 15)
        {
            var data = await Db.Posts
                .OrderByDescending(p => p.Updated)
                .OrderByDescending(p => p.Created)
                .Where(p => p.ForumId == id)
                .Select(p => new PostDTO
                {
                    Id = p.Id,
                    ForumId = p.ForumId,
                    Title = p.Title,
                    IsLocked = p.IsLocked,
                    Updated = p.Updated,
                    Created = p.Created,
                    User = new UserDTO
                    {
                        UserName = !p.User.IsBanned ? p.User.UserName : "[User Banned]",
                        IsBanned = p.User.IsBanned
                    }
                })
                .Skip((page - 1) * pageSize)
                .Take(pageSize).ToArrayAsync();

            return Ok(data);
        }

        [HttpPost]
        [Route("create")]
        public async Task<IHttpActionResult> CreateNew([FromBody]PostDTO data)
        {
            var validator = new PostCreateNewValidator();
            var validationResult = await validator.ValidateAsync(data);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);

                return BadRequest(ModelState);
            }

            var post = new Post
            {
                UserId = User.Identity.GetUserId(),
                ForumId = data.ForumId.Value,
                Title = data.Title,
                Body = data.Body,
                IsLocked = false,
                LockedReason = null,
                Updated = DateTime.Now,
                Created = DateTime.Now
            };

            var storedPost = Db.Posts.Add(post);
            await Db.SaveChangesAsync();

            return Ok(storedPost.Id);
        }

        [HttpPut]
        [Route("{id}/edit")]
        public async Task<IHttpActionResult> Edit(int id, [FromBody]PostDTO data)
        {
            var validator = new PostEditValidator();
            var validationResult = await validator.ValidateAsync(data);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);

                return BadRequest(ModelState);
            }

            var post = await Db.Posts.FindAsync(id);

            if (post == null)
                return NotFound();

            post.Title = data.Title;
            post.Body = data.Body;

            Db.Posts.Attach(post);

            var entry = Db.Entry<Post>(post);
            entry.State = EntityState.Modified;

            await Db.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete]
        [Route("{id:int}/delete")]
        public async Task<IHttpActionResult> Delete(int id)
        {
            var post = await Db.Posts.FindAsync(id);

            if (post == null)
                return NotFound();

            Db.Posts.Remove(post);

            await Db.SaveChangesAsync();

            return Ok();
        }
    }
}

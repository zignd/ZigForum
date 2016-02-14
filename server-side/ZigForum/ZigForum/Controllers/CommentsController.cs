using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using ZigForum.Controllers.Common;
using ZigForum.Models;
using ZigForum.Models.DTOs;
using ZigForum.Models.Validators;

namespace ZigForum.Controllers
{
    [Authorize]
    [RoutePrefix("api/comments")]
    public class CommentsController : ZigForumApiController
    {
        public CommentsController() : base() { }
        
        public CommentsController(ApplicationDbContext db) : base(db) { }

        [AllowAnonymous]
        [HttpGet]
        [Route("postid/{id:int}")]
        public async Task<IHttpActionResult> GetByPostId(int id, int page = 1, int pageSize = 15)
        {
            var data = await Db.Comments
                .OrderBy(p => p.Created)
                .Where(c => c.PostId == id && !c.ParentId.HasValue)
                .Select(c => new CommentDTO
                {
                    Id = c.Id,
                    PostId = c.PostId,
                    ParentId = c.ParentId,
                    Body = c.Body,
                    Updated = c.Updated,
                    Created = c.Created,
                    User = new UserDTO
                    {
                        UserName = !c.User.IsBanned ? c.User.UserName : "[User Banned]",
                        IsBanned = c.User.IsBanned
                    }
                })
                .Skip((page - 1) * pageSize)
                .Take(pageSize).ToListAsync();

            foreach (var comment in data)
            {
                await FillChildren(comment);
            }

            return Ok(data);
        }

        [HttpPost]
        [Route("create")]
        public async Task<IHttpActionResult> CreateNew([FromBody]CommentDTO data)
        {
            var validator = new CommentCreateNewValidator();
            var validationResult = await validator.ValidateAsync(data);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);

                return BadRequest(ModelState);
            }

            var comment = new Comment
            {
                UserId = User.Identity.GetUserId(),
                PostId = data.PostId.Value,
                Body = data.Body,
                Updated = DateTime.Now,
                Created = DateTime.Now
            };

            var storedComment = Db.Comments.Add(comment);
            await Db.SaveChangesAsync();

            return Ok(storedComment.Id);
        }

        [HttpPut]
        [Route("{id}/edit")]
        public async Task<IHttpActionResult> Edit(int id, [FromBody]CommentDTO data)
        {
            var validator = new CommentEditValidator();
            var validationResult = await validator.ValidateAsync(data);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);

                return BadRequest(ModelState);
            }

            var comment = await Db.Comments.FindAsync(id);

            if (comment == null)
                return NotFound();

            comment.Body = data.Body;

            Db.Comments.Attach(comment);

            var entry = Db.Entry<Comment>(comment);
            entry.State = EntityState.Modified;

            await Db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpDelete]
        [Route("{id:int}/delete")]
        public async Task<IHttpActionResult> Delete(int id)
        {
            var comment = await Db.Comments.FindAsync(id);

            if (comment == null)
                return NotFound();

            Db.Comments.Remove(comment);

            await Db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        private async Task FillChildren(CommentDTO comment)
        {
            comment.Children = await Db.Comments
                .Where(c => c.ParentId == comment.Id)
                .Select(c => new CommentDTO
                {
                    Id = c.Id,
                    PostId = c.PostId,
                    ParentId = c.ParentId,
                    Body = c.Body,
                    Updated = c.Updated,
                    Created = c.Created,
                    User = new UserDTO
                    {
                        UserName = !c.User.IsBanned ? c.User.UserName : "[User Banned]",
                        IsBanned = c.User.IsBanned
                    }
                }).ToListAsync();

            if (comment.Children.Count > 0)
            {
                foreach (var child in comment.Children)
                {
                    await FillChildren(child);
                }
            }
        }
    }
}
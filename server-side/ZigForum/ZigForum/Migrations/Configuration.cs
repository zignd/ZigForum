namespace ZigForum.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ZigForumContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        private bool AddUser(ZigForumContext context)
        {
            var userManager = new UserManager<User>(new UserStore<User>(context));

            var user = new User()
            {
                UserName = "admin",
                Created = DateTime.Now
            };

            if (userManager.FindByName(user.UserName) != null)
            {
                return true;
            }

            var identityResult = userManager.Create(user, "password");

            return identityResult.Succeeded;
        }

        private void AddPostsComments(ZigForumContext context)
        {
            var userManager = new UserManager<User>(new UserStore<User>(context));

            context.Posts.AddRange(new[]
            {
                new Post
                {
                    Title = "Title 1",
                    Body = "Body 1",
                    UserId = userManager.FindByName("admin").Id,
                    Created = DateTime.Now
                },
                new Post
                {
                    Title = "Title 2",
                    Body = "Body 2",
                    UserId = userManager.FindByName("admin").Id,
                    Created = DateTime.Now
                }
            });
        }

        protected override void Seed(ZigForumContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            AddUser(context);
            AddPostsComments(context);
        }
    }
}

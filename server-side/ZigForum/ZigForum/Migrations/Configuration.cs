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

            CreateRoles(context);
            CreateAdmin(context);

            CreateTestData(context);
        }

        private void CreateRoles(ZigForumContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            roleManager.Create(new IdentityRole("Administrator"));
            roleManager.Create(new IdentityRole("Moderator"));
        }

        private void CreateAdmin(ZigForumContext context)
        {
            var userManager = new UserManager<User>(new UserStore<User>(context));

            var admin = new User
            {
                UserName = "admin",
                Created = DateTime.Now
            };
            try
            {
                userManager.Create(admin, "password");
                admin = userManager.FindByName(admin.UserName);
                userManager.AddToRole(admin.Id, "Administrator");
            }
            catch
            {
                throw new Exception("0.1");
            }
            
        }

        private void CreateTestData(ZigForumContext context)
        {
            var userManager = new UserManager<User>(new UserStore<User>(context));

            // User
            
            var user = new User
            {
                UserName = "user1",
                Created = DateTime.Now
            };

            userManager.Create(user, "password");
            user = userManager.FindByName(user.UserName);

            // Forum

            context.Forums.Add(new Forum
            {
                Name = "Forum 1",
                Created = DateTime.Now
            });

            // Post

            context.Posts.AddRange(new[]
            {
                new Post
                {
                    UserId = user.Id,
                    Title = "Title 1",
                    Body = "Body 1",
                    Created = DateTime.Now
                },
                new Post
                {
                    UserId = user.Id,
                    Title = "Title 2",
                    Body = "Body 2",
                    Created = DateTime.Now
                }
            });
        }
    }
}

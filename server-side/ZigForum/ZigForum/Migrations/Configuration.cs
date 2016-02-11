namespace ZigForum.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ApplicationDbContext context)
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

            // Update-Database -TargetMigration Initial -Force; Add-Migration Base; Update-Database -TargetMigration Base

            CreateRoles(context);
            CreateAdmin(context);

            CreateTestData(context);
        }

        private void CreateRoles(ApplicationDbContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            roleManager.Create(new IdentityRole("Administrator"));
            roleManager.Create(new IdentityRole("Moderator"));
        }

        private void CreateAdmin(ApplicationDbContext context)
        {
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));

            var admin = new ApplicationUser
            {
                UserName = "admin",
                Created = DateTime.Now
            };

            userManager.Create(admin, "password");
            admin = userManager.FindByName(admin.UserName);
            userManager.AddToRole(admin.Id, "Administrator");
        }

        private void CreateTestData(ApplicationDbContext context)
        {
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));

            // User
            
            var user = new ApplicationUser
            {
                UserName = "user1",
                Created = DateTime.Now
            };

            userManager.Create(user, "password");
            user = userManager.FindByName(user.UserName);

            context.SaveChanges();

            // Forum

            var forum1 = context.Forums.Add(new Forum
            {
                Name = "Forum 1",
                Created = DateTime.Now
            });

            context.SaveChanges();

                var forum1dot1 = context.Forums.Add(new Forum
                {
                    ParentId = forum1.Id,
                    Name = "Forum 1.1",
                    Created = DateTime.Now
                });

                context.SaveChanges();

                    var forum1dot1dot1 = context.Forums.Add(new Forum
                    {
                        ParentId = forum1dot1.Id,
                        Name = "Forum 1.1.1",
                        Created = DateTime.Now
                    });

                    context.SaveChanges();

                var forum1dot2 = context.Forums.Add(new Forum
                {
                    ParentId = forum1.Id,
                    Name = "Forum 1.2",
                    Created = DateTime.Now
                });

                context.SaveChanges();

            var forum2 = context.Forums.Add(new Forum
            {
                Name = "Forum 2",
                Created = DateTime.Now
            });

            context.SaveChanges();

                var forum2dot1 = context.Forums.Add(new Forum
                {
                    ParentId = forum2.Id,
                    Name = "Forum 2.1",
                    Created = DateTime.Now
                });

                context.SaveChanges();

            var forum3 = context.Forums.Add(new Forum
            {
                Name = "Forum 3",
                Created = DateTime.Now
            });

            context.SaveChanges();

            // Post

            Post lastPost = null;

            for (int i = 1; i <= 100; i++)
            {
                lastPost = context.Posts.Add(new Post
                {
                    UserId = user.Id,
                    ForumId = forum1.Id,
                    Title = $"Title {i}",
                    Body = $"Body {i}",
                    Created = DateTime.Now
                });
            }

            context.SaveChanges();

            // Comment

            var comment1 = context.Comments.Add(new Comment
            {
                UserId = user.Id,
                PostId = lastPost.Id,
                Body = "Comment 1",
                Created = DateTime.Now
            });

            context.SaveChanges();

                var comment11 = context.Comments.Add(new Comment
                {
                    UserId = user.Id,
                    PostId = lastPost.Id,
                    ParentId = comment1.Id,
                    Body = "Comment 1.1",
                    Created = DateTime.Now
                });

                context.SaveChanges();

                var comment12 = context.Comments.Add(new Comment
                {
                    UserId = user.Id,
                    PostId = lastPost.Id,
                    ParentId = comment1.Id,
                    Body = "Comment 1.2",
                    Created = DateTime.Now
                });

                context.SaveChanges();

                    var comment121 = context.Comments.Add(new Comment
                    {
                        UserId = user.Id,
                        PostId = lastPost.Id,
                        ParentId = comment12.Id,
                        Body = "Comment 1.2.1",
                        Created = DateTime.Now
                    });

                    context.SaveChanges();

                    var comment122 = context.Comments.Add(new Comment
                    {
                        UserId = user.Id,
                        PostId = lastPost.Id,
                        ParentId = comment12.Id,
                        Body = "Comment 1.2.2",
                        Created = DateTime.Now
                    });

                    context.SaveChanges();

            var comment2 = context.Comments.Add(new Comment
            {
                UserId = user.Id,
                PostId = lastPost.Id,
                Body = "Comment 2",
                Created = DateTime.Now
            });

            context.SaveChanges();
        }
    }
}

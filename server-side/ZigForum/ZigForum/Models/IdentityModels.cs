using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ZigForum.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public bool IsBanned { get; set; }
        public DateTime Created { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public virtual DbSet<Forum> Forums { get; set; }

        public virtual DbSet<Post> Posts { get; set; }

        public virtual DbSet<PostHistory> PostsHistory { get; set; }

        public virtual DbSet<PostVote> PostsVotes { get; set; }

        public virtual DbSet<Comment> Comments { get; set; }

        public virtual DbSet<CommentHistory> CommentsHistory { get; set; }

        public virtual DbSet<CommentVote> CommentsVotes { get; set; }

        public virtual DbSet<ForumModerator> ForumsModerators { get; set; }

        public virtual DbSet<Ban> Bans { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            // Forum

            modelBuilder.Entity<Forum>()
                .HasKey(f => f.Id);

            modelBuilder.Entity<Forum>()
                .HasOptional(f => f.Parent)
                .WithMany(f => f.SubForums)
                .HasForeignKey(f => f.ParentId);

            modelBuilder.Entity<Forum>()
                .Property(f => f.Name).IsRequired();

            modelBuilder.Entity<Forum>()
                .Property(f => f.Created).IsRequired();

            // Post

            modelBuilder.Entity<Post>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<Post>()
                .HasRequired(p => p.User)
                .WithMany()
                .HasForeignKey(p => p.UserId);

            modelBuilder.Entity<Post>()
                .HasRequired(p => p.Forum)
                .WithMany(f => f.Posts)
                .HasForeignKey(p => p.ForumId);

            modelBuilder.Entity<Post>()
                .Property(p => p.UserId).IsRequired();

            modelBuilder.Entity<Post>()
                .Property(p => p.ForumId).IsRequired();

            modelBuilder.Entity<Post>()
                .Property(p => p.Title).IsRequired();

            modelBuilder.Entity<Post>()
                .Property(p => p.Body).IsRequired();

            modelBuilder.Entity<Post>()
                .Property(p => p.Created).IsRequired();

            // PostHistory

            modelBuilder.Entity<PostHistory>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<PostHistory>()
                .HasRequired(p => p.Post)
                .WithMany()
                .HasForeignKey(p => p.PostId);

            modelBuilder.Entity<PostHistory>()
                .Property(p => p.PostId).IsRequired();

            modelBuilder.Entity<PostHistory>()
                .Property(p => p.Title).IsRequired();

            modelBuilder.Entity<PostHistory>()
                .Property(p => p.Body).IsRequired();

            modelBuilder.Entity<PostHistory>()
                .Property(p => p.Created).IsRequired();

            // PostVote

            modelBuilder.Entity<PostVote>()
                .HasKey(p => new { p.PostId, p.UserAuthorId, p.UserTargetId });

            modelBuilder.Entity<PostVote>()
                .HasRequired(p => p.Post)
                .WithMany()
                .HasForeignKey(p => p.PostId);

            modelBuilder.Entity<PostVote>()
                .HasRequired(p => p.UserAuthor)
                .WithMany()
                .HasForeignKey(p => p.UserAuthorId);

            modelBuilder.Entity<PostVote>()
                .HasRequired(p => p.UserTarget)
                .WithMany()
                .HasForeignKey(p => p.UserTargetId);

            modelBuilder.Entity<PostVote>()
                .Property(p => p.PostId).IsRequired();

            modelBuilder.Entity<PostVote>()
                .Property(p => p.UserAuthorId).IsRequired();

            modelBuilder.Entity<PostVote>()
                .Property(p => p.UserTargetId).IsRequired();

            modelBuilder.Entity<PostVote>()
                .Property(p => p.Created).IsRequired();

            // Comment

            modelBuilder.Entity<Comment>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<Comment>()
                .HasRequired(c => c.Post)
                .WithMany(p => p.Comments)
                .HasForeignKey(c => c.PostId);

            modelBuilder.Entity<Comment>()
                .HasRequired(c => c.User)
                .WithMany()
                .HasForeignKey(c => c.UserId);

            modelBuilder.Entity<Comment>()
                .HasOptional(c => c.Parent)
                .WithMany(c => c.Children)
                .HasForeignKey(c => c.ParentId);

            modelBuilder.Entity<Comment>()
                .Property(c => c.PostId).IsRequired();

            modelBuilder.Entity<Comment>()
                .Property(c => c.UserId).IsRequired();

            modelBuilder.Entity<Comment>()
                .Property(c => c.Body).IsRequired();

            modelBuilder.Entity<Comment>()
                .Property(c => c.Created).IsRequired();

            // CommentHistory

            modelBuilder.Entity<CommentHistory>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<CommentHistory>()
                .HasRequired(c => c.Comment)
                .WithMany()
                .HasForeignKey(c => c.CommentId);

            modelBuilder.Entity<CommentHistory>()
                .Property(c => c.CommentId).IsRequired();

            modelBuilder.Entity<CommentHistory>()
                .Property(c => c.Body).IsRequired();

            modelBuilder.Entity<CommentHistory>()
                .Property(c => c.Created).IsRequired();

            // CommentVote

            modelBuilder.Entity<CommentVote>()
                .HasKey(c => new { c.CommentId, c.UserAuthorId, c.UserTargetId });

            modelBuilder.Entity<CommentVote>()
                .HasRequired(c => c.Comment)
                .WithMany()
                .HasForeignKey(c => c.CommentId);

            modelBuilder.Entity<CommentVote>()
                .HasRequired(c => c.UserAuthor)
                .WithMany()
                .HasForeignKey(c => c.UserAuthorId);

            modelBuilder.Entity<CommentVote>()
                .HasRequired(c => c.UserTarget)
                .WithMany()
                .HasForeignKey(c => c.UserTargetId);

            modelBuilder.Entity<CommentVote>()
                .Property(c => c.CommentId).IsRequired();

            modelBuilder.Entity<CommentVote>()
                .Property(c => c.UserAuthorId).IsRequired();

            modelBuilder.Entity<CommentVote>()
                .Property(c => c.UserTargetId).IsRequired();

            modelBuilder.Entity<CommentVote>()
                .Property(c => c.Created).IsRequired();

            // ForumModerator

            modelBuilder.Entity<ForumModerator>()
                .HasKey(f => new { f.ForumId, f.UserId });

            modelBuilder.Entity<ForumModerator>()
                .HasRequired(f => f.Forum)
                .WithMany()
                .HasForeignKey(f => f.ForumId);

            modelBuilder.Entity<ForumModerator>()
                .HasRequired(f => f.User)
                .WithMany()
                .HasForeignKey(f => f.UserId);

            modelBuilder.Entity<ForumModerator>()
                .Property(f => f.UserId).IsRequired();

            modelBuilder.Entity<ForumModerator>()
                .Property(f => f.ForumId).IsRequired();

            modelBuilder.Entity<ForumModerator>()
                .Property(f => f.Created).IsRequired();

            // Ban

            modelBuilder.Entity<Ban>()
                .HasKey(b => b.Id);

            modelBuilder.Entity<Ban>()
                .HasRequired(b => b.User)
                .WithMany()
                .HasForeignKey(b => b.UserId);

            modelBuilder.Entity<Ban>()
                .Property(b => b.UserId).IsRequired();

            modelBuilder.Entity<Ban>()
                .Property(b => b.Reason).IsRequired();

            modelBuilder.Entity<Ban>()
                .Property(b => b.BannedUntil).IsRequired();

            modelBuilder.Entity<Ban>()
                .Property(b => b.Created).IsRequired();
        }
    }
}

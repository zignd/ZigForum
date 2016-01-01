using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZigForum.Models
{
    /// <summary>
    /// Context for accessing the datastore
    /// </summary>
    /// <remarks>
    /// This class inherits from IdentityDbContext which provides the
    /// DbSet<> for User and IdentityRole.
    /// </remarks>
    public class ZigForumContext : IdentityDbContext<User>
    {
        /// <summary>
        /// Creates a new instance of the context
        /// </summary>
        /// <remarks>
        /// This constructor assumes that the context will be named
        /// "ZigForumContext" in your Web.config file.
        /// </remarks>
        public ZigForumContext() : base("ZigForumContext")
        {
            // Yeah dynamic proxies were disabled and here's the reason:
            // http://johnnycode.com/2012/04/10/serializing-circular-references-with-json-net-and-entity-framework/
            Configuration.ProxyCreationEnabled = false;
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

        /// <summary>
        /// Uses the fluent API to explicitly define some relations in the schema
        /// </summary>
        /// <param name="modelBuilder">Supplied by Entity Framework</param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToOneConstraintIntroductionConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            // Forum

            modelBuilder.Entity<Forum>()
                .HasKey(f => f.Id);

            modelBuilder.Entity<Forum>()
                .HasOptional(f => f.Parent);

            modelBuilder.Entity<Forum>()
                .Property(f => f.Name).IsRequired();

            modelBuilder.Entity<Forum>()
                .Property(f => f.Created).IsRequired();

            // Post

            modelBuilder.Entity<Post>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<Post>()
                .HasRequired(p => p.User);

            modelBuilder.Entity<Post>()
                .Property(p => p.UserId).IsRequired();

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
                .HasRequired(p => p.Post);

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
                .HasRequired(p => p.Post);
            
            modelBuilder.Entity<PostVote>()
                .HasRequired(p => p.UserAuthor);
            
            modelBuilder.Entity<PostVote>()
                .HasRequired(p => p.UserTarget);

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
                .HasRequired(c => c.Post);

            modelBuilder.Entity<Comment>()
                .HasRequired(c => c.User);

            modelBuilder.Entity<Comment>()
                .HasRequired(c => c.Parent);

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
                .HasRequired(c => c.Comment);

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
                .HasRequired(c => c.Comment);

            modelBuilder.Entity<CommentVote>()
                .HasRequired(c => c.UserAuthor);

            modelBuilder.Entity<CommentVote>()
                .HasRequired(c => c.UserTarget);

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
                .Property(f => f.UserId).IsRequired();

            modelBuilder.Entity<ForumModerator>()
                .Property(f => f.ForumId).IsRequired();

            modelBuilder.Entity<ForumModerator>()
                .Property(f => f.Created).IsRequired();

            // Ban

            modelBuilder.Entity<Ban>()
                .HasKey(b => b.Id);

            modelBuilder.Entity<Ban>()
                .HasRequired(b => b.User);

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

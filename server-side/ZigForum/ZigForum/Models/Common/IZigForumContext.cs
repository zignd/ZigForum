using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZigForum.Models.Common
{
    public interface IZigForumContext : IDisposable
    {
        DbSet<Forum> Forums { get; set; }

        DbSet<Post> Posts { get; set; }

        DbSet<PostHistory> PostsHistory { get; set; }

        DbSet<PostVote> PostsVotes { get; set; }

        DbSet<Comment> Comments { get; set; }

        DbSet<CommentHistory> CommentsHistory { get; set; }

        DbSet<CommentVote> CommentsVotes { get; set; }

        DbSet<ForumModerator> ForumsModerators { get; set; }

        DbSet<Ban> Bans { get; set; }

        int SaveChanges();

        Task<int> SaveChangesAsync();

        void MarkAsModified(object item);
    }
}

namespace ZigForum.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Base : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            CreateTable(
                "dbo.Ban",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        IsActive = c.Boolean(nullable: false),
                        Reason = c.String(nullable: false),
                        BannedUntil = c.DateTime(nullable: false),
                        Created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Comment",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PostId = c.Int(nullable: false),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ParentId = c.Int(nullable: false),
                        Body = c.String(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        Updated = c.DateTime(),
                        Created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Comment", t => t.ParentId)
                .ForeignKey("dbo.Post", t => t.PostId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.PostId)
                .Index(t => t.UserId)
                .Index(t => t.ParentId);
            
            CreateTable(
                "dbo.Post",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        Title = c.String(nullable: false),
                        Body = c.String(nullable: false),
                        IsLocked = c.Boolean(nullable: false),
                        LockedReason = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        Updated = c.DateTime(),
                        Created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.CommentHistory",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CommentId = c.Int(nullable: false),
                        Body = c.String(nullable: false),
                        Created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Comment", t => t.CommentId)
                .Index(t => t.CommentId);
            
            CreateTable(
                "dbo.CommentVote",
                c => new
                    {
                        CommentId = c.Int(nullable: false),
                        UserAuthorId = c.String(nullable: false, maxLength: 128),
                        UserTargetId = c.String(nullable: false, maxLength: 128),
                        IsUpVote = c.Boolean(nullable: false),
                        Created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.CommentId, t.UserAuthorId, t.UserTargetId })
                .ForeignKey("dbo.Comment", t => t.CommentId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserAuthorId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserTargetId)
                .Index(t => t.CommentId)
                .Index(t => t.UserAuthorId)
                .Index(t => t.UserTargetId);
            
            CreateTable(
                "dbo.Forum",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ParentId = c.Int(),
                        Name = c.String(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        Created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Forum", t => t.ParentId)
                .Index(t => t.ParentId);
            
            CreateTable(
                "dbo.ForumModerator",
                c => new
                    {
                        ForumId = c.Int(nullable: false),
                        UserId = c.String(nullable: false, maxLength: 128),
                        Created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.ForumId, t.UserId })
                .ForeignKey("dbo.Forum", t => t.ForumId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.ForumId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.PostHistory",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PostId = c.Int(nullable: false),
                        Title = c.String(nullable: false),
                        Body = c.String(nullable: false),
                        Created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Post", t => t.PostId)
                .Index(t => t.PostId);
            
            CreateTable(
                "dbo.PostVote",
                c => new
                    {
                        PostId = c.Int(nullable: false),
                        UserAuthorId = c.String(nullable: false, maxLength: 128),
                        UserTargetId = c.String(nullable: false, maxLength: 128),
                        IsUpVote = c.Boolean(nullable: false),
                        Created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.PostId, t.UserAuthorId, t.UserTargetId })
                .ForeignKey("dbo.Post", t => t.PostId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserAuthorId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserTargetId)
                .Index(t => t.PostId)
                .Index(t => t.UserAuthorId)
                .Index(t => t.UserTargetId);
            
            AddForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles", "Id");
            AddForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.PostVote", "UserTargetId", "dbo.AspNetUsers");
            DropForeignKey("dbo.PostVote", "UserAuthorId", "dbo.AspNetUsers");
            DropForeignKey("dbo.PostVote", "PostId", "dbo.Post");
            DropForeignKey("dbo.PostHistory", "PostId", "dbo.Post");
            DropForeignKey("dbo.ForumModerator", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ForumModerator", "ForumId", "dbo.Forum");
            DropForeignKey("dbo.Forum", "ParentId", "dbo.Forum");
            DropForeignKey("dbo.CommentVote", "UserTargetId", "dbo.AspNetUsers");
            DropForeignKey("dbo.CommentVote", "UserAuthorId", "dbo.AspNetUsers");
            DropForeignKey("dbo.CommentVote", "CommentId", "dbo.Comment");
            DropForeignKey("dbo.CommentHistory", "CommentId", "dbo.Comment");
            DropForeignKey("dbo.Comment", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Comment", "PostId", "dbo.Post");
            DropForeignKey("dbo.Post", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Comment", "ParentId", "dbo.Comment");
            DropForeignKey("dbo.Ban", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.PostVote", new[] { "UserTargetId" });
            DropIndex("dbo.PostVote", new[] { "UserAuthorId" });
            DropIndex("dbo.PostVote", new[] { "PostId" });
            DropIndex("dbo.PostHistory", new[] { "PostId" });
            DropIndex("dbo.ForumModerator", new[] { "UserId" });
            DropIndex("dbo.ForumModerator", new[] { "ForumId" });
            DropIndex("dbo.Forum", new[] { "ParentId" });
            DropIndex("dbo.CommentVote", new[] { "UserTargetId" });
            DropIndex("dbo.CommentVote", new[] { "UserAuthorId" });
            DropIndex("dbo.CommentVote", new[] { "CommentId" });
            DropIndex("dbo.CommentHistory", new[] { "CommentId" });
            DropIndex("dbo.Post", new[] { "UserId" });
            DropIndex("dbo.Comment", new[] { "ParentId" });
            DropIndex("dbo.Comment", new[] { "UserId" });
            DropIndex("dbo.Comment", new[] { "PostId" });
            DropIndex("dbo.Ban", new[] { "UserId" });
            DropTable("dbo.PostVote");
            DropTable("dbo.PostHistory");
            DropTable("dbo.ForumModerator");
            DropTable("dbo.Forum");
            DropTable("dbo.CommentVote");
            DropTable("dbo.CommentHistory");
            DropTable("dbo.Post");
            DropTable("dbo.Comment");
            DropTable("dbo.Ban");
            AddForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles", "Id", cascadeDelete: true);
        }
    }
}

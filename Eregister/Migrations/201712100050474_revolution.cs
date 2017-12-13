namespace Eregister.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class revolution : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false),
                        UrlSeo = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        Checked = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PostCategories",
                c => new
                    {
                        PostId = c.String(nullable: false, maxLength: 128),
                        CategoryId = c.String(nullable: false, maxLength: 128),
                        Checked = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.PostId, t.CategoryId })
                .ForeignKey("dbo.Categories", t => t.CategoryId)
                .ForeignKey("dbo.Posts", t => t.PostId)
                .Index(t => t.PostId)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Title = c.String(nullable: false),
                        ShortDescription = c.String(nullable: false),
                        Body = c.String(nullable: false),
                        Meta = c.String(nullable: false),
                        UrlSeo = c.String(nullable: false),
                        Published = c.Boolean(nullable: false),
                        NetLikeCount = c.Int(nullable: false),
                        PostedOn = c.DateTime(nullable: false),
                        Modified = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        PageId = c.String(),
                        DateTime = c.DateTime(nullable: false),
                        UserName = c.String(),
                        Body = c.String(nullable: false),
                        NetLikeCount = c.Int(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        Post_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Posts", t => t.Post_Id)
                .Index(t => t.Post_Id);
            
            CreateTable(
                "dbo.CommentLikes",
                c => new
                    {
                        CommentId = c.String(nullable: false, maxLength: 128),
                        Username = c.String(),
                        Like = c.Boolean(nullable: false),
                        Dislike = c.Boolean(nullable: false),
                        Comment_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.CommentId)
                .ForeignKey("dbo.Comments", t => t.Comment_Id)
                .Index(t => t.Comment_Id);
            
            CreateTable(
                "dbo.Replies",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        CommentId = c.String(maxLength: 128),
                        ParentReplyId = c.String(),
                        DateTime = c.DateTime(nullable: false),
                        UserName = c.String(),
                        Body = c.String(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        Post_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Comments", t => t.CommentId)
                .ForeignKey("dbo.Posts", t => t.Post_Id)
                .Index(t => t.CommentId)
                .Index(t => t.Post_Id);
            
            CreateTable(
                "dbo.ReplyLikes",
                c => new
                    {
                        ReplyId = c.String(nullable: false, maxLength: 128),
                        Username = c.String(),
                        Like = c.Boolean(nullable: false),
                        Dislike = c.Boolean(nullable: false),
                        Reply_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ReplyId)
                .ForeignKey("dbo.Replies", t => t.Reply_Id)
                .Index(t => t.Reply_Id);
            
            CreateTable(
                "dbo.PostLikes",
                c => new
                    {
                        PostId = c.String(nullable: false, maxLength: 128),
                        Username = c.String(),
                        Like = c.Boolean(nullable: false),
                        Dislike = c.Boolean(nullable: false),
                        Post_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.PostId)
                .ForeignKey("dbo.Posts", t => t.Post_Id)
                .Index(t => t.Post_Id);
            
            CreateTable(
                "dbo.PostTags",
                c => new
                    {
                        PostId = c.String(nullable: false, maxLength: 128),
                        TagId = c.String(nullable: false, maxLength: 128),
                        Checked = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.PostId, t.TagId })
                .ForeignKey("dbo.Posts", t => t.PostId)
                .ForeignKey("dbo.Tags", t => t.TagId)
                .Index(t => t.PostId)
                .Index(t => t.TagId);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false),
                        UrlSeo = c.String(nullable: false),
                        Checked = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PostVideos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VideoUrl = c.String(nullable: false),
                        VideoThumbnail = c.String(),
                        PostId = c.String(maxLength: 128),
                        VideoSiteName = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Posts", t => t.PostId)
                .Index(t => t.PostId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PostVideos", "PostId", "dbo.Posts");
            DropForeignKey("dbo.PostTags", "TagId", "dbo.Tags");
            DropForeignKey("dbo.PostTags", "PostId", "dbo.Posts");
            DropForeignKey("dbo.PostLikes", "Post_Id", "dbo.Posts");
            DropForeignKey("dbo.PostCategories", "PostId", "dbo.Posts");
            DropForeignKey("dbo.ReplyLikes", "Reply_Id", "dbo.Replies");
            DropForeignKey("dbo.Replies", "Post_Id", "dbo.Posts");
            DropForeignKey("dbo.Replies", "CommentId", "dbo.Comments");
            DropForeignKey("dbo.Comments", "Post_Id", "dbo.Posts");
            DropForeignKey("dbo.CommentLikes", "Comment_Id", "dbo.Comments");
            DropForeignKey("dbo.PostCategories", "CategoryId", "dbo.Categories");
            DropIndex("dbo.PostVideos", new[] { "PostId" });
            DropIndex("dbo.PostTags", new[] { "TagId" });
            DropIndex("dbo.PostTags", new[] { "PostId" });
            DropIndex("dbo.PostLikes", new[] { "Post_Id" });
            DropIndex("dbo.ReplyLikes", new[] { "Reply_Id" });
            DropIndex("dbo.Replies", new[] { "Post_Id" });
            DropIndex("dbo.Replies", new[] { "CommentId" });
            DropIndex("dbo.CommentLikes", new[] { "Comment_Id" });
            DropIndex("dbo.Comments", new[] { "Post_Id" });
            DropIndex("dbo.PostCategories", new[] { "CategoryId" });
            DropIndex("dbo.PostCategories", new[] { "PostId" });
            DropTable("dbo.PostVideos");
            DropTable("dbo.Tags");
            DropTable("dbo.PostTags");
            DropTable("dbo.PostLikes");
            DropTable("dbo.ReplyLikes");
            DropTable("dbo.Replies");
            DropTable("dbo.CommentLikes");
            DropTable("dbo.Comments");
            DropTable("dbo.Posts");
            DropTable("dbo.PostCategories");
            DropTable("dbo.Categories");
        }
    }
}

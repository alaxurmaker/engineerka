namespace Eregister.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Iinit : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        AddressID = c.Int(nullable: false, identity: true),
                        Street = c.String(),
                        City = c.String(),
                        PostalCode = c.String(),
                        Country = c.String(),
                        Phone = c.String(),
                    })
                .PrimaryKey(t => t.AddressID);
            
            CreateTable(
                "dbo.Parents",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        Phone = c.String(),
                        AddressID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.Addresses", t => t.AddressID)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.AddressID);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        BirthDate = c.DateTime(nullable: false),
                        Country = c.String(),
                        EmailLinkDate = c.DateTime(nullable: false),
                        FirstName = c.String(),
                        JoinDate = c.DateTime(nullable: false),
                        LastLoginDate = c.DateTime(nullable: false),
                        LastName = c.String(),
                        PersonSex = c.String(),
                        TokenValue = c.String(),
                        TokenIsValid = c.Boolean(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        Pesel = c.Int(),
                        JoinDate = c.DateTime(),
                        AddressID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.Addresses", t => t.AddressID)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.AddressID);
            
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        GroupID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.GroupID)
                .ForeignKey("dbo.Students", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.StudentGrades",
                c => new
                    {
                        StudentGradeID = c.Int(nullable: false, identity: true),
                        Grade = c.String(),
                        UserId = c.String(maxLength: 128),
                        SubjectID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.StudentGradeID)
                .ForeignKey("dbo.Students", t => t.UserId)
                .ForeignKey("dbo.Subjects", t => t.SubjectID)
                .Index(t => t.UserId)
                .Index(t => t.SubjectID);
            
            CreateTable(
                "dbo.FinalGrades",
                c => new
                    {
                        FinalGradeID = c.Int(nullable: false, identity: true),
                        GradeValue = c.Int(nullable: false),
                        TextValue = c.String(),
                        ConductGrade = c.Int(),
                        Description = c.String(),
                        StudentGradeID = c.Int(nullable: false),
                        SeasonID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FinalGradeID)
                .ForeignKey("dbo.Seasons", t => t.SeasonID)
                .ForeignKey("dbo.StudentGrades", t => t.StudentGradeID)
                .Index(t => t.StudentGradeID)
                .Index(t => t.SeasonID);
            
            CreateTable(
                "dbo.Seasons",
                c => new
                    {
                        SeasonID = c.Int(nullable: false, identity: true),
                        Semester = c.Int(nullable: false),
                        StartDate = c.DateTime(),
                        EndDate = c.DateTime(),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t.SeasonID);
            
            CreateTable(
                "dbo.StudentHistories",
                c => new
                    {
                        StudentHistoryID = c.Int(nullable: false, identity: true),
                        Status = c.String(),
                        SeasonID = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.StudentHistoryID)
                .ForeignKey("dbo.Seasons", t => t.SeasonID)
                .ForeignKey("dbo.Students", t => t.UserId)
                .Index(t => t.SeasonID)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Subjects",
                c => new
                    {
                        SubjectID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.SubjectID);
            
            CreateTable(
                "dbo.GradeRatings",
                c => new
                    {
                        GradeRatingID = c.Int(nullable: false, identity: true),
                        TestType = c.String(),
                        Wage = c.Single(nullable: false),
                        SubjectID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.GradeRatingID)
                .ForeignKey("dbo.Subjects", t => t.SubjectID)
                .Index(t => t.SubjectID);
            
            CreateTable(
                "dbo.TeacherSubjects",
                c => new
                    {
                        TeacherSubjectID = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        SubjectID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TeacherSubjectID)
                .ForeignKey("dbo.Subjects", t => t.SubjectID)
                .ForeignKey("dbo.Teachers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.SubjectID);
            
            CreateTable(
                "dbo.Teachers",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        Title = c.String(),
                        AddressID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.Addresses", t => t.AddressID)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.AddressID);
            
            CreateTable(
                "dbo.StudentParents",
                c => new
                    {
                        StudentParentID = c.Int(nullable: false, identity: true),
                        StudentID = c.String(maxLength: 128),
                        ParentID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.StudentParentID)
                .ForeignKey("dbo.Parents", t => t.ParentID)
                .ForeignKey("dbo.Students", t => t.StudentID)
                .Index(t => t.StudentID)
                .Index(t => t.ParentID);
            
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
                        UserName = c.String(nullable: false),
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
            
            CreateTable(
                "dbo.Quizs",
                c => new
                    {
                        QuizID = c.Int(nullable: false, identity: true),
                        Question = c.String(),
                        Correct = c.Int(nullable: false),
                        Answer1 = c.String(),
                        Answer2 = c.String(),
                        Answer3 = c.String(),
                        Answer4 = c.String(),
                        Score = c.Int(),
                    })
                .PrimaryKey(t => t.QuizID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.Rooms",
                c => new
                    {
                        RoomID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Type = c.String(),
                        Capacity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RoomID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
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
            DropForeignKey("dbo.Parents", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.StudentParents", "StudentID", "dbo.Students");
            DropForeignKey("dbo.StudentParents", "ParentID", "dbo.Parents");
            DropForeignKey("dbo.StudentGrades", "SubjectID", "dbo.Subjects");
            DropForeignKey("dbo.TeacherSubjects", "UserId", "dbo.Teachers");
            DropForeignKey("dbo.Teachers", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Teachers", "AddressID", "dbo.Addresses");
            DropForeignKey("dbo.TeacherSubjects", "SubjectID", "dbo.Subjects");
            DropForeignKey("dbo.GradeRatings", "SubjectID", "dbo.Subjects");
            DropForeignKey("dbo.StudentGrades", "UserId", "dbo.Students");
            DropForeignKey("dbo.FinalGrades", "StudentGradeID", "dbo.StudentGrades");
            DropForeignKey("dbo.FinalGrades", "SeasonID", "dbo.Seasons");
            DropForeignKey("dbo.StudentHistories", "UserId", "dbo.Students");
            DropForeignKey("dbo.StudentHistories", "SeasonID", "dbo.Seasons");
            DropForeignKey("dbo.Groups", "UserId", "dbo.Students");
            DropForeignKey("dbo.Students", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Students", "AddressID", "dbo.Addresses");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Parents", "AddressID", "dbo.Addresses");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
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
            DropIndex("dbo.StudentParents", new[] { "ParentID" });
            DropIndex("dbo.StudentParents", new[] { "StudentID" });
            DropIndex("dbo.Teachers", new[] { "AddressID" });
            DropIndex("dbo.Teachers", new[] { "UserId" });
            DropIndex("dbo.TeacherSubjects", new[] { "SubjectID" });
            DropIndex("dbo.TeacherSubjects", new[] { "UserId" });
            DropIndex("dbo.GradeRatings", new[] { "SubjectID" });
            DropIndex("dbo.StudentHistories", new[] { "UserId" });
            DropIndex("dbo.StudentHistories", new[] { "SeasonID" });
            DropIndex("dbo.FinalGrades", new[] { "SeasonID" });
            DropIndex("dbo.FinalGrades", new[] { "StudentGradeID" });
            DropIndex("dbo.StudentGrades", new[] { "SubjectID" });
            DropIndex("dbo.StudentGrades", new[] { "UserId" });
            DropIndex("dbo.Groups", new[] { "UserId" });
            DropIndex("dbo.Students", new[] { "AddressID" });
            DropIndex("dbo.Students", new[] { "UserId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Parents", new[] { "AddressID" });
            DropIndex("dbo.Parents", new[] { "UserId" });
            DropTable("dbo.Rooms");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Quizs");
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
            DropTable("dbo.StudentParents");
            DropTable("dbo.Teachers");
            DropTable("dbo.TeacherSubjects");
            DropTable("dbo.GradeRatings");
            DropTable("dbo.Subjects");
            DropTable("dbo.StudentHistories");
            DropTable("dbo.Seasons");
            DropTable("dbo.FinalGrades");
            DropTable("dbo.StudentGrades");
            DropTable("dbo.Groups");
            DropTable("dbo.Students");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Parents");
            DropTable("dbo.Addresses");
        }
    }
}

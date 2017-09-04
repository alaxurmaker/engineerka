namespace Eregister.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
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
                    })
                .PrimaryKey(t => t.AddressID);
            
            CreateTable(
                "dbo.Parents",
                c => new
                    {
                        ParentID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Phone = c.String(),
                        AddressID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ParentID)
                .ForeignKey("dbo.Addresses", t => t.AddressID, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: true)
                .Index(t => t.AddressID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.StudentParents",
                c => new
                    {
                        StudentParentID = c.Int(nullable: false, identity: true),
                        StudentID = c.Int(nullable: false),
                        ParentID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.StudentParentID)
                .ForeignKey("dbo.Parents", t => t.ParentID, cascadeDelete: true)
                .ForeignKey("dbo.Students", t => t.StudentID, cascadeDelete: true)
                .Index(t => t.StudentID)
                .Index(t => t.ParentID);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        StudentID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Surname = c.String(),
                        Pesel = c.Int(nullable: false),
                        BirthDate = c.DateTime(nullable: false),
                        JoinDate = c.DateTime(nullable: false),
                        Classroom = c.String(),
                        AddressID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                        EducatorID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.StudentID)
                .ForeignKey("dbo.Addresses", t => t.AddressID, cascadeDelete: true)
                .ForeignKey("dbo.Educators", t => t.EducatorID, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: true)
                .Index(t => t.AddressID)
                .Index(t => t.UserID)
                .Index(t => t.EducatorID);
            
            CreateTable(
                "dbo.Educators",
                c => new
                    {
                        EducatorID = c.Int(nullable: false, identity: true),
                        TeacherID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EducatorID)
                .ForeignKey("dbo.Teachers", t => t.TeacherID, cascadeDelete: true)
                .Index(t => t.TeacherID);
            
            CreateTable(
                "dbo.Teachers",
                c => new
                    {
                        TeacherID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Stringname = c.String(),
                        Title = c.String(),
                        Phone = c.String(),
                        AddressID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TeacherID)
                .ForeignKey("dbo.Addresses", t => t.AddressID, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: true)
                .Index(t => t.AddressID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        GroupID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        TeacherID = c.Int(nullable: false),
                        SubjectID = c.Int(nullable: false),
                        StudentID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.GroupID)
                .ForeignKey("dbo.Students", t => t.StudentID, cascadeDelete: true)
                .ForeignKey("dbo.Subjects", t => t.SubjectID, cascadeDelete: true)
                .ForeignKey("dbo.Teachers", t => t.TeacherID, cascadeDelete: true)
                .Index(t => t.TeacherID)
                .Index(t => t.SubjectID)
                .Index(t => t.StudentID);
            
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
                .ForeignKey("dbo.Subjects", t => t.SubjectID, cascadeDelete: true)
                .Index(t => t.SubjectID);
            
            CreateTable(
                "dbo.StudentGrades",
                c => new
                    {
                        StudentGradeID = c.Int(nullable: false, identity: true),
                        StudentID = c.Int(nullable: false),
                        SubjectID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.StudentGradeID)
                .ForeignKey("dbo.Students", t => t.StudentID, cascadeDelete: true)
                .ForeignKey("dbo.Subjects", t => t.SubjectID, cascadeDelete: true)
                .Index(t => t.StudentID)
                .Index(t => t.SubjectID);
            
            CreateTable(
                "dbo.FinalGrades",
                c => new
                    {
                        FinalGradeID = c.Int(nullable: false, identity: true),
                        Value = c.Int(nullable: false),
                        TextValue = c.String(),
                        ConductGrade = c.Int(),
                        Note = c.String(),
                        StudentGradeID = c.Int(nullable: false),
                        SeasonID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FinalGradeID)
                .ForeignKey("dbo.Seasons", t => t.SeasonID, cascadeDelete: true)
                .ForeignKey("dbo.StudentGrades", t => t.StudentGradeID, cascadeDelete: true)
                .Index(t => t.StudentGradeID)
                .Index(t => t.SeasonID);
            
            CreateTable(
                "dbo.Seasons",
                c => new
                    {
                        SeasonID = c.Int(nullable: false, identity: true),
                        Semester = c.Int(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
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
                        StudentID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.StudentHistoryID)
                .ForeignKey("dbo.Seasons", t => t.SeasonID, cascadeDelete: true)
                .ForeignKey("dbo.Students", t => t.StudentID, cascadeDelete: true)
                .Index(t => t.SeasonID)
                .Index(t => t.StudentID);
            
            CreateTable(
                "dbo.TeacherSubjects",
                c => new
                    {
                        TeacherSubjectID = c.Int(nullable: false, identity: true),
                        TeacherID = c.Int(nullable: false),
                        SubjectID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TeacherSubjectID)
                .ForeignKey("dbo.Subjects", t => t.SubjectID, cascadeDelete: true)
                .ForeignKey("dbo.Teachers", t => t.TeacherID, cascadeDelete: true)
                .Index(t => t.TeacherID)
                .Index(t => t.SubjectID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        Login = c.String(),
                        Password = c.String(),
                        PasswordSalt = c.String(),
                        PasswordHash = c.String(),
                        Role = c.String(),
                    })
                .PrimaryKey(t => t.UserID);
            
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
            DropForeignKey("dbo.StudentParents", "StudentID", "dbo.Students");
            DropForeignKey("dbo.Teachers", "UserID", "dbo.Users");
            DropForeignKey("dbo.Students", "UserID", "dbo.Users");
            DropForeignKey("dbo.Parents", "UserID", "dbo.Users");
            DropForeignKey("dbo.Groups", "TeacherID", "dbo.Teachers");
            DropForeignKey("dbo.TeacherSubjects", "TeacherID", "dbo.Teachers");
            DropForeignKey("dbo.TeacherSubjects", "SubjectID", "dbo.Subjects");
            DropForeignKey("dbo.StudentGrades", "SubjectID", "dbo.Subjects");
            DropForeignKey("dbo.StudentGrades", "StudentID", "dbo.Students");
            DropForeignKey("dbo.FinalGrades", "StudentGradeID", "dbo.StudentGrades");
            DropForeignKey("dbo.StudentHistories", "StudentID", "dbo.Students");
            DropForeignKey("dbo.StudentHistories", "SeasonID", "dbo.Seasons");
            DropForeignKey("dbo.FinalGrades", "SeasonID", "dbo.Seasons");
            DropForeignKey("dbo.Groups", "SubjectID", "dbo.Subjects");
            DropForeignKey("dbo.GradeRatings", "SubjectID", "dbo.Subjects");
            DropForeignKey("dbo.Groups", "StudentID", "dbo.Students");
            DropForeignKey("dbo.Educators", "TeacherID", "dbo.Teachers");
            DropForeignKey("dbo.Teachers", "AddressID", "dbo.Addresses");
            DropForeignKey("dbo.Students", "EducatorID", "dbo.Educators");
            DropForeignKey("dbo.Students", "AddressID", "dbo.Addresses");
            DropForeignKey("dbo.StudentParents", "ParentID", "dbo.Parents");
            DropForeignKey("dbo.Parents", "AddressID", "dbo.Addresses");
            DropIndex("dbo.TeacherSubjects", new[] { "SubjectID" });
            DropIndex("dbo.TeacherSubjects", new[] { "TeacherID" });
            DropIndex("dbo.StudentHistories", new[] { "StudentID" });
            DropIndex("dbo.StudentHistories", new[] { "SeasonID" });
            DropIndex("dbo.FinalGrades", new[] { "SeasonID" });
            DropIndex("dbo.FinalGrades", new[] { "StudentGradeID" });
            DropIndex("dbo.StudentGrades", new[] { "SubjectID" });
            DropIndex("dbo.StudentGrades", new[] { "StudentID" });
            DropIndex("dbo.GradeRatings", new[] { "SubjectID" });
            DropIndex("dbo.Groups", new[] { "StudentID" });
            DropIndex("dbo.Groups", new[] { "SubjectID" });
            DropIndex("dbo.Groups", new[] { "TeacherID" });
            DropIndex("dbo.Teachers", new[] { "UserID" });
            DropIndex("dbo.Teachers", new[] { "AddressID" });
            DropIndex("dbo.Educators", new[] { "TeacherID" });
            DropIndex("dbo.Students", new[] { "EducatorID" });
            DropIndex("dbo.Students", new[] { "UserID" });
            DropIndex("dbo.Students", new[] { "AddressID" });
            DropIndex("dbo.StudentParents", new[] { "ParentID" });
            DropIndex("dbo.StudentParents", new[] { "StudentID" });
            DropIndex("dbo.Parents", new[] { "UserID" });
            DropIndex("dbo.Parents", new[] { "AddressID" });
            DropTable("dbo.Rooms");
            DropTable("dbo.Users");
            DropTable("dbo.TeacherSubjects");
            DropTable("dbo.StudentHistories");
            DropTable("dbo.Seasons");
            DropTable("dbo.FinalGrades");
            DropTable("dbo.StudentGrades");
            DropTable("dbo.GradeRatings");
            DropTable("dbo.Subjects");
            DropTable("dbo.Groups");
            DropTable("dbo.Teachers");
            DropTable("dbo.Educators");
            DropTable("dbo.Students");
            DropTable("dbo.StudentParents");
            DropTable("dbo.Parents");
            DropTable("dbo.Addresses");
        }
    }
}

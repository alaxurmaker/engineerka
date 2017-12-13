namespace Eregister.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class JoinStudentToIdentity : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Parents", "UserID", "dbo.Users");
            DropForeignKey("dbo.Teachers", "UserID", "dbo.Users");
            DropForeignKey("dbo.StudentParents", "StudentID", "dbo.Students");
            DropForeignKey("dbo.Groups", "StudentID", "dbo.Students");
            DropForeignKey("dbo.StudentGrades", "StudentID", "dbo.Students");
            DropForeignKey("dbo.StudentHistories", "StudentID", "dbo.Students");
            DropIndex("dbo.Parents", new[] { "UserID" });
            DropIndex("dbo.StudentParents", new[] { "StudentID" });
            DropIndex("dbo.Students", new[] { "AddressID" });
            DropIndex("dbo.Students", new[] { "UserID" });
            DropIndex("dbo.Students", new[] { "EducatorID" });
            DropIndex("dbo.Teachers", new[] { "UserID" });
            DropIndex("dbo.Groups", new[] { "StudentID" });
            DropIndex("dbo.StudentGrades", new[] { "StudentID" });
            DropIndex("dbo.StudentHistories", new[] { "StudentID" });
            DropPrimaryKey("dbo.Students");
            AddColumn("dbo.StudentParents", "Student_UserId", c => c.String(maxLength: 128));
            AddColumn("dbo.Students", "FirstName", c => c.String());
            AddColumn("dbo.Groups", "Student_UserId", c => c.String(maxLength: 128));
            AddColumn("dbo.StudentGrades", "Student_UserId", c => c.String(maxLength: 128));
            AddColumn("dbo.StudentHistories", "Student_UserId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Students", "Pesel", c => c.Int());
            AlterColumn("dbo.Students", "BirthDate", c => c.DateTime());
            AlterColumn("dbo.Students", "JoinDate", c => c.DateTime());
            AlterColumn("dbo.Students", "AddressID", c => c.Int());
            AlterColumn("dbo.Students", "UserId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Students", "EducatorID", c => c.Int());
            AddPrimaryKey("dbo.Students", "UserId");
            CreateIndex("dbo.StudentParents", "Student_UserId");
            CreateIndex("dbo.Students", "UserId");
            CreateIndex("dbo.Students", "AddressID");
            CreateIndex("dbo.Students", "EducatorID");
            CreateIndex("dbo.Groups", "Student_UserId");
            CreateIndex("dbo.StudentGrades", "Student_UserId");
            CreateIndex("dbo.StudentHistories", "Student_UserId");
            AddForeignKey("dbo.StudentParents", "Student_UserId", "dbo.Students", "UserId");
            AddForeignKey("dbo.Groups", "Student_UserId", "dbo.Students", "UserId");
            AddForeignKey("dbo.StudentGrades", "Student_UserId", "dbo.Students", "UserId");
            AddForeignKey("dbo.StudentHistories", "Student_UserId", "dbo.Students", "UserId");
            DropColumn("dbo.Students", "StudentID");
           // DropTable("dbo.Users");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserID = c.Int(nullable: false),
                        Email = c.String(),
                        Login = c.String(),
                        Password = c.String(),
                        PasswordSalt = c.String(),
                        PasswordHash = c.String(),
                        Role = c.String(),
                        Created = c.DateTime(nullable: false),
                        LastLogin = c.DateTime(nullable: false),
                        ImageId = c.Int(nullable: false, identity: true),
                        ImageTitle = c.String(),
                        ImageByte = c.Binary(),
                        ImagePath = c.String(),
                    })
                .PrimaryKey(t => t.UserID);
            
            AddColumn("dbo.Students", "StudentID", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.StudentHistories", "Student_UserId", "dbo.Students");
            DropForeignKey("dbo.StudentGrades", "Student_UserId", "dbo.Students");
            DropForeignKey("dbo.Groups", "Student_UserId", "dbo.Students");
            DropForeignKey("dbo.StudentParents", "Student_UserId", "dbo.Students");
            DropIndex("dbo.StudentHistories", new[] { "Student_UserId" });
            DropIndex("dbo.StudentGrades", new[] { "Student_UserId" });
            DropIndex("dbo.Groups", new[] { "Student_UserId" });
            DropIndex("dbo.Students", new[] { "EducatorID" });
            DropIndex("dbo.Students", new[] { "AddressID" });
            DropIndex("dbo.Students", new[] { "UserId" });
            DropIndex("dbo.StudentParents", new[] { "Student_UserId" });
            DropPrimaryKey("dbo.Students");
            AlterColumn("dbo.Students", "EducatorID", c => c.Int(nullable: false));
            AlterColumn("dbo.Students", "UserId", c => c.Int(nullable: false));
            AlterColumn("dbo.Students", "AddressID", c => c.Int(nullable: false));
            AlterColumn("dbo.Students", "JoinDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Students", "BirthDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Students", "Pesel", c => c.Int(nullable: false));
            DropColumn("dbo.StudentHistories", "Student_UserId");
            DropColumn("dbo.StudentGrades", "Student_UserId");
            DropColumn("dbo.Groups", "Student_UserId");
            DropColumn("dbo.Students", "FirstName");
            DropColumn("dbo.StudentParents", "Student_UserId");
            AddPrimaryKey("dbo.Students", "StudentID");
            CreateIndex("dbo.StudentHistories", "StudentID");
            CreateIndex("dbo.StudentGrades", "StudentID");
            CreateIndex("dbo.Groups", "StudentID");
            CreateIndex("dbo.Teachers", "UserID");
            CreateIndex("dbo.Students", "EducatorID");
            CreateIndex("dbo.Students", "UserID");
            CreateIndex("dbo.Students", "AddressID");
            CreateIndex("dbo.StudentParents", "StudentID");
            CreateIndex("dbo.Parents", "UserID");
            AddForeignKey("dbo.StudentHistories", "StudentID", "dbo.Students", "StudentID");
            AddForeignKey("dbo.StudentGrades", "StudentID", "dbo.Students", "StudentID");
            AddForeignKey("dbo.Groups", "StudentID", "dbo.Students", "StudentID");
            AddForeignKey("dbo.StudentParents", "StudentID", "dbo.Students", "StudentID");
            AddForeignKey("dbo.Teachers", "UserID", "dbo.Users", "UserID");
            AddForeignKey("dbo.Parents", "UserID", "dbo.Users", "UserID");
        }
    }
}

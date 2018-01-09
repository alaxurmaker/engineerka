namespace Eregister.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStudentSubjectTableFix : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StudentSubjects",
                c => new
                    {
                        StudentSubjectID = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        SubjectID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.StudentSubjectID)
                .ForeignKey("dbo.Students", t => t.UserId)
                .ForeignKey("dbo.Subjects", t => t.SubjectID)
                .Index(t => t.UserId)
                .Index(t => t.SubjectID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StudentSubjects", "SubjectID", "dbo.Subjects");
            DropForeignKey("dbo.StudentSubjects", "UserId", "dbo.Students");
            DropIndex("dbo.StudentSubjects", new[] { "SubjectID" });
            DropIndex("dbo.StudentSubjects", new[] { "UserId" });
            DropTable("dbo.StudentSubjects");
        }
    }
}

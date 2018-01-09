namespace Eregister.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StudentSubjectChangesRemoveSubjects : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Subjects", "Student_UserId", "dbo.Students");
            DropIndex("dbo.Subjects", new[] { "Student_UserId" });
            DropColumn("dbo.Subjects", "Student_UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Subjects", "Student_UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Subjects", "Student_UserId");
            AddForeignKey("dbo.Subjects", "Student_UserId", "dbo.Students", "UserId");
        }
    }
}

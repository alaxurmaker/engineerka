namespace Eregister.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StudentSubjectChanges : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Subjects", "Student_UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Subjects", "Student_UserId");
            AddForeignKey("dbo.Subjects", "Student_UserId", "dbo.Students", "UserId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Subjects", "Student_UserId", "dbo.Students");
            DropIndex("dbo.Subjects", new[] { "Student_UserId" });
            DropColumn("dbo.Subjects", "Student_UserId");
        }
    }
}

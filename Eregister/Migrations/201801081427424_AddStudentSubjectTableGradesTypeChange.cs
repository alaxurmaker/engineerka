namespace Eregister.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStudentSubjectTableGradesTypeChange : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StudentGrades", "Grades", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.StudentGrades", "Grades");
        }
    }
}

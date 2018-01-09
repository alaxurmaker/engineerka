namespace Eregister.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSubjectDirectToTeacherSubject : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TeacherSubjects", "SubjectDirectTo", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TeacherSubjects", "SubjectDirectTo");
        }
    }
}

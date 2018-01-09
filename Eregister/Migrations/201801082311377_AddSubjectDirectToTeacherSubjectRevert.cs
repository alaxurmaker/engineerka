namespace Eregister.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSubjectDirectToTeacherSubjectRevert : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.TeacherSubjects", "SubjectDirectTo");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TeacherSubjects", "SubjectDirectTo", c => c.String());
        }
    }
}

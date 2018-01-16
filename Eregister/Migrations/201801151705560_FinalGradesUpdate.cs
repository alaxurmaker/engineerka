namespace Eregister.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FinalGradesUpdate : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.FinalGrades", new[] { "SeasonID" });
            RenameColumn(table: "dbo.FinalGrades", name: "SeasonID", newName: "Season_SeasonID");
            AddColumn("dbo.FinalGrades", "AddDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.FinalGrades", "GradeValue", c => c.String());
            AlterColumn("dbo.FinalGrades", "Season_SeasonID", c => c.Int());
            CreateIndex("dbo.FinalGrades", "Season_SeasonID");
            DropColumn("dbo.FinalGrades", "TextValue");
            DropColumn("dbo.FinalGrades", "Description");
        }
        
        public override void Down()
        {
            AddColumn("dbo.FinalGrades", "Description", c => c.String());
            AddColumn("dbo.FinalGrades", "TextValue", c => c.String());
            DropIndex("dbo.FinalGrades", new[] { "Season_SeasonID" });
            AlterColumn("dbo.FinalGrades", "Season_SeasonID", c => c.Int(nullable: false));
            AlterColumn("dbo.FinalGrades", "GradeValue", c => c.Int(nullable: false));
            DropColumn("dbo.FinalGrades", "AddDate");
            RenameColumn(table: "dbo.FinalGrades", name: "Season_SeasonID", newName: "SeasonID");
            CreateIndex("dbo.FinalGrades", "SeasonID");
        }
    }
}

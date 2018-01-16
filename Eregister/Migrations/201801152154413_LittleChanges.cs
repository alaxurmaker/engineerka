namespace Eregister.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LittleChanges : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.GradeRatings", "SubjectID", "dbo.Subjects");
            DropIndex("dbo.GradeRatings", new[] { "SubjectID" });
            DropTable("dbo.GradeRatings");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.GradeRatings",
                c => new
                    {
                        GradeRatingID = c.Int(nullable: false, identity: true),
                        TestType = c.String(),
                        Wage = c.Single(nullable: false),
                        SubjectID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.GradeRatingID);
            
            CreateIndex("dbo.GradeRatings", "SubjectID");
            AddForeignKey("dbo.GradeRatings", "SubjectID", "dbo.Subjects", "SubjectID");
        }
    }
}

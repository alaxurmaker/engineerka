namespace Eregister.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EducatorCheckAddToGroup5 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Teachers", "GroupID", "dbo.Groups");
            DropIndex("dbo.Teachers", new[] { "GroupID" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.Teachers", "GroupID");
            AddForeignKey("dbo.Teachers", "GroupID", "dbo.Groups", "GroupID");
        }
    }
}

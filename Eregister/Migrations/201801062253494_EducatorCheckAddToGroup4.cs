namespace Eregister.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EducatorCheckAddToGroup4 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Groups", "Group_GroupID", "dbo.Groups");
            DropIndex("dbo.Groups", new[] { "Group_GroupID" });
            DropColumn("dbo.Groups", "Group_GroupID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Groups", "Group_GroupID", c => c.Int());
            CreateIndex("dbo.Groups", "Group_GroupID");
            AddForeignKey("dbo.Groups", "Group_GroupID", "dbo.Groups", "GroupID");
        }
    }
}

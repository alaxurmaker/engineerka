namespace Eregister.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GroupsChange : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Groups", "UserId", "dbo.Students");
            DropIndex("dbo.Groups", new[] { "UserId" });
            AddColumn("dbo.Students", "GroupID", c => c.Int());
            CreateIndex("dbo.Students", "GroupID");
            AddForeignKey("dbo.Students", "GroupID", "dbo.Groups", "GroupID");
            DropColumn("dbo.Groups", "UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Groups", "UserId", c => c.String(maxLength: 128));
            DropForeignKey("dbo.Students", "GroupID", "dbo.Groups");
            DropIndex("dbo.Students", new[] { "GroupID" });
            DropColumn("dbo.Students", "GroupID");
            CreateIndex("dbo.Groups", "UserId");
            AddForeignKey("dbo.Groups", "UserId", "dbo.Students", "UserId");
        }
    }
}

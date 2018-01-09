namespace Eregister.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TeacherGroup : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Teachers", "GroupID", c => c.Int());
            AddColumn("dbo.Groups", "Group_GroupID", c => c.Int());
            CreateIndex("dbo.Teachers", "GroupID");
            CreateIndex("dbo.Groups", "Group_GroupID");
            AddForeignKey("dbo.Groups", "Group_GroupID", "dbo.Groups", "GroupID");
            AddForeignKey("dbo.Teachers", "GroupID", "dbo.Groups", "GroupID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Teachers", "GroupID", "dbo.Groups");
            DropForeignKey("dbo.Groups", "Group_GroupID", "dbo.Groups");
            DropIndex("dbo.Groups", new[] { "Group_GroupID" });
            DropIndex("dbo.Teachers", new[] { "GroupID" });
            DropColumn("dbo.Groups", "Group_GroupID");
            DropColumn("dbo.Teachers", "GroupID");
        }
    }
}

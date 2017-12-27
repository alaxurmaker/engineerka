namespace Eregister.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNotification : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Teachers", "AlertID", "dbo.Alerts");
            DropForeignKey("dbo.Students", "AlertID", "dbo.Alerts");
            DropIndex("dbo.Students", new[] { "AlertID" });
            DropIndex("dbo.Teachers", new[] { "AlertID" });
            AddColumn("dbo.Alerts", "StudentUserId", c => c.String(maxLength: 128));
            AddColumn("dbo.Alerts", "TeacherUserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Alerts", "StudentUserId");
            CreateIndex("dbo.Alerts", "TeacherUserId");
            AddForeignKey("dbo.Alerts", "StudentUserId", "dbo.Students", "UserId");
            AddForeignKey("dbo.Alerts", "TeacherUserId", "dbo.Teachers", "UserId");
            DropColumn("dbo.Students", "AlertID");
            DropColumn("dbo.Alerts", "Count");
            DropColumn("dbo.Teachers", "AlertID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Teachers", "AlertID", c => c.Int());
            AddColumn("dbo.Alerts", "Count", c => c.Int(nullable: false));
            AddColumn("dbo.Students", "AlertID", c => c.Int());
            DropForeignKey("dbo.Alerts", "TeacherUserId", "dbo.Teachers");
            DropForeignKey("dbo.Alerts", "StudentUserId", "dbo.Students");
            DropIndex("dbo.Alerts", new[] { "TeacherUserId" });
            DropIndex("dbo.Alerts", new[] { "StudentUserId" });
            DropColumn("dbo.Alerts", "TeacherUserId");
            DropColumn("dbo.Alerts", "StudentUserId");
            CreateIndex("dbo.Teachers", "AlertID");
            CreateIndex("dbo.Students", "AlertID");
            AddForeignKey("dbo.Students", "AlertID", "dbo.Alerts", "AlertID");
            AddForeignKey("dbo.Teachers", "AlertID", "dbo.Alerts", "AlertID");
        }
    }
}

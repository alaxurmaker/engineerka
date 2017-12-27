namespace Eregister.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Alerts : DbMigration
    {
        public override void Up()
        {
        //    DropForeignKey("dbo.AspNetUsers", "AlertID", "dbo.Alerts");
       //     DropIndex("dbo.AspNetUsers", new[] { "AlertID" });
            AddColumn("dbo.Students", "AlertID", c => c.Int());
            AddColumn("dbo.Teachers", "AlertID", c => c.Int());
            CreateIndex("dbo.Students", "AlertID");
            CreateIndex("dbo.Teachers", "AlertID");
            AddForeignKey("dbo.Teachers", "AlertID", "dbo.Alerts", "AlertID");
            AddForeignKey("dbo.Students", "AlertID", "dbo.Alerts", "AlertID");
     //       DropColumn("dbo.AspNetUsers", "AlertID");
        }
        
        public override void Down()
        {
        //    AddColumn("dbo.AspNetUsers", "AlertID", c => c.Int(nullable: false));
            DropForeignKey("dbo.Students", "AlertID", "dbo.Alerts");
            DropForeignKey("dbo.Teachers", "AlertID", "dbo.Alerts");
            DropIndex("dbo.Teachers", new[] { "AlertID" });
            DropIndex("dbo.Students", new[] { "AlertID" });
            DropColumn("dbo.Teachers", "AlertID");
            DropColumn("dbo.Students", "AlertID");
        //    CreateIndex("dbo.AspNetUsers", "AlertID");
         //   AddForeignKey("dbo.AspNetUsers", "AlertID", "dbo.Alerts", "AlertID");
        }
    }
}

namespace Eregister.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NotificationsUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Alerts", "AddDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Alerts", "AddDate");
        }
    }
}

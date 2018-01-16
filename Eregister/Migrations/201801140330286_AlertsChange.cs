namespace Eregister.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlertsChange : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Alerts", "AddDate", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Alerts", "AddDate", c => c.DateTime());
        }
    }
}

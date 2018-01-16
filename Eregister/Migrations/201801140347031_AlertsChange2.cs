namespace Eregister.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlertsChange2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Alerts", "AddDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Alerts", "AddDate", c => c.String());
        }
    }
}

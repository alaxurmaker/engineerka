namespace Eregister.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMessagesUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Messages", "UserFrom", c => c.String());
            AddColumn("dbo.Messages", "IsOn", c => c.Boolean(nullable: false));
            AddColumn("dbo.Messages", "AddDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Messages", "AddDate");
            DropColumn("dbo.Messages", "IsOn");
            DropColumn("dbo.Messages", "UserFrom");
        }
    }
}

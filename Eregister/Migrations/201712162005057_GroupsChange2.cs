namespace Eregister.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GroupsChange2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Groups", "Year", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Groups", "Year");
        }
    }
}

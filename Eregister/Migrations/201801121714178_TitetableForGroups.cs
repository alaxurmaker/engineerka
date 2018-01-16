namespace Eregister.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TitetableForGroups : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Events", "group_id", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Events", "group_id");
        }
    }
}

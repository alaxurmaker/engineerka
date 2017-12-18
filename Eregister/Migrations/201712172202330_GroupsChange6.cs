namespace Eregister.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GroupsChange6 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Students", "GroupName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Students", "GroupName");
        }
    }
}

namespace Eregister.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GroupsChange4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "NameSurname", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "NameSurname");
        }
    }
}

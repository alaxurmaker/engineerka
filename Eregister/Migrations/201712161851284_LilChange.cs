namespace Eregister.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LilChange : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "NameSurname");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "NameSurname", c => c.String());
        }
    }
}

namespace Eregister.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSkins : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "CustomSkin", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "CustomSkin");
        }
    }
}

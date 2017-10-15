namespace Eregister.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Third_migration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "ImageId", c => c.Int(nullable: false));
            AddColumn("dbo.Users", "ImageTitle", c => c.String());
            AddColumn("dbo.Users", "ImageByte", c => c.Binary());
            AddColumn("dbo.Users", "ImagePath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "ImagePath");
            DropColumn("dbo.Users", "ImageByte");
            DropColumn("dbo.Users", "ImageTitle");
            DropColumn("dbo.Users", "ImageId");
        }
    }
}

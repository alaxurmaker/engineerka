namespace Eregister.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserEditProfilePhotos : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "ImageByte", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "ImageByte");
        }
    }
}

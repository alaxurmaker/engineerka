namespace Eregister.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedUserToPost : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "UserName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "UserName");
        }
    }
}

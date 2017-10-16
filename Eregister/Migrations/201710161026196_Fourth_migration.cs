namespace Eregister.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Fourth_migration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Parents", "UserID", "dbo.Users");
            DropForeignKey("dbo.Students", "UserID", "dbo.Users");
            DropForeignKey("dbo.Teachers", "UserID", "dbo.Users");
            DropPrimaryKey("dbo.Users");
            AlterColumn("dbo.Users", "UserID", c => c.Int(nullable: false));
            AlterColumn("dbo.Users", "ImageId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Users", "UserID");
            AddForeignKey("dbo.Parents", "UserID", "dbo.Users", "UserID");
            AddForeignKey("dbo.Students", "UserID", "dbo.Users", "UserID");
            AddForeignKey("dbo.Teachers", "UserID", "dbo.Users", "UserID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Teachers", "UserID", "dbo.Users");
            DropForeignKey("dbo.Students", "UserID", "dbo.Users");
            DropForeignKey("dbo.Parents", "UserID", "dbo.Users");
            DropPrimaryKey("dbo.Users");
            AlterColumn("dbo.Users", "ImageId", c => c.Int(nullable: false));
            AlterColumn("dbo.Users", "UserID", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Users", "UserID");
            AddForeignKey("dbo.Teachers", "UserID", "dbo.Users", "UserID");
            AddForeignKey("dbo.Students", "UserID", "dbo.Users", "UserID");
            AddForeignKey("dbo.Parents", "UserID", "dbo.Users", "UserID");
        }
    }
}

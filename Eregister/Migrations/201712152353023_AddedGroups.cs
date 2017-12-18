namespace Eregister.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedGroups : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Parents", new[] { "AddressID" });
            DropIndex("dbo.Students", new[] { "AddressID" });
            DropIndex("dbo.Teachers", new[] { "AddressID" });
            AlterColumn("dbo.Parents", "AddressID", c => c.Int());
            AlterColumn("dbo.Students", "AddressID", c => c.Int());
            AlterColumn("dbo.Teachers", "AddressID", c => c.Int());
            CreateIndex("dbo.Parents", "AddressID");
            CreateIndex("dbo.Students", "AddressID");
            CreateIndex("dbo.Teachers", "AddressID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Teachers", new[] { "AddressID" });
            DropIndex("dbo.Students", new[] { "AddressID" });
            DropIndex("dbo.Parents", new[] { "AddressID" });
            AlterColumn("dbo.Teachers", "AddressID", c => c.Int(nullable: false));
            AlterColumn("dbo.Students", "AddressID", c => c.Int(nullable: false));
            AlterColumn("dbo.Parents", "AddressID", c => c.Int(nullable: false));
            CreateIndex("dbo.Teachers", "AddressID");
            CreateIndex("dbo.Students", "AddressID");
            CreateIndex("dbo.Parents", "AddressID");
        }
    }
}

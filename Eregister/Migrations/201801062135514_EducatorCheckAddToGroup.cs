namespace Eregister.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EducatorCheckAddToGroup : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Groups", "OwnsEducator", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Groups", "OwnsEducator");
        }
    }
}

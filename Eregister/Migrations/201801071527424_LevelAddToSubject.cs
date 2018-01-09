namespace Eregister.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LevelAddToSubject : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Subjects", "Level", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Subjects", "Level");
        }
    }
}

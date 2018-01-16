namespace Eregister.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTeacherAuthorization : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "TokenValueAdv", c => c.String());
            AddColumn("dbo.AspNetUsers", "TokenAdvIsValid", c => c.Boolean(nullable: false));
            AddColumn("dbo.Teachers", "Pesel", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Teachers", "Pesel");
            DropColumn("dbo.AspNetUsers", "TokenAdvIsValid");
            DropColumn("dbo.AspNetUsers", "TokenValueAdv");
        }
    }
}

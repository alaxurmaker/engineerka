namespace Eregister.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedNameSurnameApplicationUser2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Students", "Pesel", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Students", "Pesel", c => c.Int());
        }
    }
}

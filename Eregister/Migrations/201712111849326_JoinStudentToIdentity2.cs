namespace Eregister.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class JoinStudentToIdentity2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "PersonSex", c => c.String());
            DropColumn("dbo.Students", "BirthDate");
            DropColumn("dbo.Students", "JoinDate");
            DropColumn("dbo.Students", "Sex");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Students", "Sex", c => c.Int());
            AddColumn("dbo.Students", "JoinDate", c => c.DateTime());
            AddColumn("dbo.Students", "BirthDate", c => c.DateTime());
            DropColumn("dbo.AspNetUsers", "PersonSex");
        }
    }
}

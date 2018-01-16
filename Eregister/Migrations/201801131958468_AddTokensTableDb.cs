namespace Eregister.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTokensTableDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tokens",
                c => new
                    {
                        TokenId = c.Int(nullable: false, identity: true),
                        Role = c.String(),
                        Code = c.String(),
                    })
                .PrimaryKey(t => t.TokenId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Tokens");
        }
    }
}

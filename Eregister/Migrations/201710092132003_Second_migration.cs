namespace Eregister.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Second_migration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Quizs",
                c => new
                    {
                        QuizID = c.Int(nullable: false, identity: true),
                        Question = c.String(),
                        Correct = c.Int(nullable: false),
                        Answer1 = c.String(),
                        Answer2 = c.String(),
                        Answer3 = c.String(),
                        Answer4 = c.String(),
                        Score = c.Int(),
                    })
                .PrimaryKey(t => t.QuizID);
            
            AddColumn("dbo.Students", "Sex", c => c.Int());
            AddColumn("dbo.Users", "Created", c => c.DateTime(nullable: false));
            AddColumn("dbo.Users", "LastLogin", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "LastLogin");
            DropColumn("dbo.Users", "Created");
            DropColumn("dbo.Students", "Sex");
            DropTable("dbo.Quizs");
        }
    }
}

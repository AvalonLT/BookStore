namespace BookStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeLanguageFromModels : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Books", "Language");
            DropTable("dbo.Languages");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Languages",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            AddColumn("dbo.Books", "Language", c => c.String(nullable: false));
        }
    }
}

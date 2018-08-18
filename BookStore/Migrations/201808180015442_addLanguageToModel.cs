namespace BookStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addLanguageToModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Languages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Books", "LanguageId", c => c.Int(nullable: false));
            CreateIndex("dbo.Books", "LanguageId");
            AddForeignKey("dbo.Books", "LanguageId", "dbo.Languages", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Books", "LanguageId", "dbo.Languages");
            DropIndex("dbo.Books", new[] { "LanguageId" });
            DropColumn("dbo.Books", "LanguageId");
            DropTable("dbo.Languages");
        }
    }
}

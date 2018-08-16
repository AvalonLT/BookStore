namespace BookStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addPropLanguage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "Language", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Books", "Language");
        }
    }
}

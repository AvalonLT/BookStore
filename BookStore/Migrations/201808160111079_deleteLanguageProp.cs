namespace BookStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deleteLanguageProp : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Books", "Language");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Books", "Language", c => c.Int(nullable: false));
        }
    }
}

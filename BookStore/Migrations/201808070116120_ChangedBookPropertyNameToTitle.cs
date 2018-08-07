namespace BookStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedBookPropertyNameToTitle : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "Title", c => c.String(nullable: false));
            DropColumn("dbo.Books", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Books", "Name", c => c.String(nullable: false));
            DropColumn("dbo.Books", "Title");
        }
    }
}

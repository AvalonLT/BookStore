namespace BookStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addChangePropLanguageClass : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Languages", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Languages", "Name");
        }
    }
}

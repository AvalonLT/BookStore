namespace BookStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameSearchResultToSearchInput : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.SearchResults", newName: "SearchInputs");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.SearchInputs", newName: "SearchResults");
        }
    }
}

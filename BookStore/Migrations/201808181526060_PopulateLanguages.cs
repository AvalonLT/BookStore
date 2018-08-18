namespace BookStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateLanguages : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Languages (Name) VALUES ('English')");
            Sql("INSERT INTO Languages (Name) VALUES ('German')");
            Sql("INSERT INTO Languages (Name) VALUES ('French')");
            Sql("INSERT INTO Languages (Name) VALUES ('Spanish')");
            Sql("INSERT INTO Languages (Name) VALUES ('Lithuanian')");
        }
        
        public override void Down()
        {
        }
    }
}

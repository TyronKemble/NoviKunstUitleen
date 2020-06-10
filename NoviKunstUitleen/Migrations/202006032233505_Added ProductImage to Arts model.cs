namespace NoviKunstUitleen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedProductImagetoArtsmodel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Arts", "ProductImage", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Arts", "ProductImage");
        }
    }
}

namespace NoviKunstUitleen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedProductImagetoArtsmodel : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Arts", "ProductImage");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Arts", "ProductImage", c => c.Binary());
        }
    }
}

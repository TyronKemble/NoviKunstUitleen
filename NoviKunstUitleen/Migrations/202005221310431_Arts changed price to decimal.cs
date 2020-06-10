namespace NoviKunstUitleen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Artschangedpricetodecimal : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Arts", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Arts", "Price", c => c.Byte(nullable: false));
        }
    }
}

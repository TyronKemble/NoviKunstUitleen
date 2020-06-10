namespace NoviKunstUitleen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DataannotationArtLendPeriodnodataannotation : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ArtLendPeriods", "EndOfLend", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ArtLendPeriods", "EndOfLend", c => c.String(nullable: false));
        }
    }
}

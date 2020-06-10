namespace NoviKunstUitleen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedModelartLend : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ArtLendPeriods", "ArtsId", "dbo.Arts");
            DropIndex("dbo.ArtLendPeriods", new[] { "ArtsId" });
            AlterColumn("dbo.ArtLendPeriods", "ArtsId", c => c.Int());
            CreateIndex("dbo.ArtLendPeriods", "ArtsId");
            AddForeignKey("dbo.ArtLendPeriods", "ArtsId", "dbo.Arts", "ArtsId");
            DropColumn("dbo.ArtLendPeriods", "StartOfLend");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ArtLendPeriods", "StartOfLend", c => c.String());
            DropForeignKey("dbo.ArtLendPeriods", "ArtsId", "dbo.Arts");
            DropIndex("dbo.ArtLendPeriods", new[] { "ArtsId" });
            AlterColumn("dbo.ArtLendPeriods", "ArtsId", c => c.Int(nullable: false));
            CreateIndex("dbo.ArtLendPeriods", "ArtsId");
            AddForeignKey("dbo.ArtLendPeriods", "ArtsId", "dbo.Arts", "ArtsId", cascadeDelete: true);
        }
    }
}

namespace NoviKunstUitleen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deletedartLendPeriodtoCart : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Carts", "ArtLendPeriod_Id", "dbo.ArtLendPeriods");
            DropIndex("dbo.Carts", new[] { "ArtLendPeriod_Id" });
            DropColumn("dbo.Carts", "ArtLendPeriod_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Carts", "ArtLendPeriod_Id", c => c.Int());
            CreateIndex("dbo.Carts", "ArtLendPeriod_Id");
            AddForeignKey("dbo.Carts", "ArtLendPeriod_Id", "dbo.ArtLendPeriods", "ArtLendPeriod_Id");
        }
    }
}

namespace NoviKunstUitleen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedorderDetailModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderDetails", "ArtLendPeriod_Id", c => c.Int());
            CreateIndex("dbo.OrderDetails", "ArtLendPeriod_Id");
            AddForeignKey("dbo.OrderDetails", "ArtLendPeriod_Id", "dbo.ArtLendPeriods", "ArtLendPeriod_Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderDetails", "ArtLendPeriod_Id", "dbo.ArtLendPeriods");
            DropIndex("dbo.OrderDetails", new[] { "ArtLendPeriod_Id" });
            DropColumn("dbo.OrderDetails", "ArtLendPeriod_Id");
        }
    }
}

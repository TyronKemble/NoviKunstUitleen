namespace NoviKunstUitleen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedendOfLoantoOrderDetail : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OrderDetails", "ArtLendPeriod_Id", "dbo.ArtLendPeriods");
            DropIndex("dbo.OrderDetails", new[] { "ArtLendPeriod_Id" });
            AddColumn("dbo.OrderDetails", "EndOfLoan", c => c.String());
            DropColumn("dbo.OrderDetails", "ArtLendPeriod_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OrderDetails", "ArtLendPeriod_Id", c => c.Int());
            DropColumn("dbo.OrderDetails", "EndOfLoan");
            CreateIndex("dbo.OrderDetails", "ArtLendPeriod_Id");
            AddForeignKey("dbo.OrderDetails", "ArtLendPeriod_Id", "dbo.ArtLendPeriods", "ArtLendPeriod_Id");
        }
    }
}

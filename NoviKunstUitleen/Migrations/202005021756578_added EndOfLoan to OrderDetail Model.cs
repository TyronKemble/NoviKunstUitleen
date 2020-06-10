namespace NoviKunstUitleen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedEndOfLoantoOrderDetailModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderDetails", "EndOfLoan", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrderDetails", "EndOfLoan");
        }
    }
}

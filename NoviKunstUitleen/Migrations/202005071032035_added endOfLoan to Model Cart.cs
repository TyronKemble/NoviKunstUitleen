namespace NoviKunstUitleen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedendOfLoantoModelCart : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Carts", "EndOfLoan", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Carts", "EndOfLoan");
        }
    }
}

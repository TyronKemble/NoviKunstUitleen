namespace NoviKunstUitleen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCryptoWalletToApplicationUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Wallet_Wallet", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Wallet_Wallet");
        }
    }
}

namespace NoviKunstUitleen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCryptoWalletToApplicationUser1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "CryptoWallet", c => c.String());
            DropColumn("dbo.AspNetUsers", "Wallet_Wallet");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Wallet_Wallet", c => c.String());
            DropColumn("dbo.AspNetUsers", "CryptoWallet");
        }
    }
}

namespace NoviKunstUitleen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCryptoModelToApplicationUser1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "CryptoWallet", c => c.String());
            DropColumn("dbo.AspNetUsers", "CryptoWallet_CryptoWallet");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "CryptoWallet_CryptoWallet", c => c.String());
            DropColumn("dbo.AspNetUsers", "CryptoWallet");
        }
    }
}

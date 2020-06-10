namespace NoviKunstUitleen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CheckChanges : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StaffMembers", "CryptoWallet", c => c.String());
            AddColumn("dbo.Students", "CryptoWallet", c => c.String());
            DropColumn("dbo.StaffMembers", "CryptoUWallet");
            DropColumn("dbo.Students", "CryptoUWallet");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Students", "CryptoUWallet", c => c.String());
            AddColumn("dbo.StaffMembers", "CryptoUWallet", c => c.String());
            DropColumn("dbo.Students", "CryptoWallet");
            DropColumn("dbo.StaffMembers", "CryptoWallet");
        }
    }
}

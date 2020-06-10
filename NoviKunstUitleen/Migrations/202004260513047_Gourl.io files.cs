namespace NoviKunstUitleen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Gourliofiles : DbMigration
    {
        public override void Up()
        {
            this.Sql(@"CREATE TABLE dbo.crypto_payments (
                   paymentID int IDENTITY(1, 1) NOT NULL,
                   boxID int NOT NULL,
                   boxType nvarchar(10) NOT NULL,
                   orderID varchar(50) NOT NULL,
                   userID varchar(50) NOT NULL,
                   countryID varchar(3) NOT NULL,
                   coinLabel varchar(6) NOT NULL,
                   amount decimal(20, 8) NOT NULL,
                   amountUSD decimal(20, 8) NOT NULL,
                   unrecognised tinyint NOT NULL,
                   addr nvarchar(50) NOT NULL,
                   txID char(64) NOT NULL,
                   txDate datetime NULL,
                   txConfirmed tinyint NOT NULL,
                   txCheckDate datetime NULL,
                   processed tinyint NOT NULL,
                   processedDate datetime NULL,
                   recordCreated datetime NULL)");
        }
        
        public override void Down()
        {
        }
    }
}

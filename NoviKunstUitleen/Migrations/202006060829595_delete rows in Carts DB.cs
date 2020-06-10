namespace NoviKunstUitleen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deleterowsinCartsDB : DbMigration
    {
        public override void Up()
        {
            Sql("DELETE FROM CARTS WHERE RecordID = 351");

        }
        
        public override void Down()
        {
        }
    }
}

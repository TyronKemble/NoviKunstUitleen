namespace NoviKunstUitleen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deleterowsfromDBwithSQL : DbMigration
    {
        public override void Up()
        {
            Sql("DELETE FROM CARTS WHERE RecordID= 353");
            Sql("DELETE FROM CARTS WHERE RecordID= 364");
            Sql("DELETE FROM CARTS WHERE RecordID= 365");
            Sql("DELETE FROM CARTS WHERE RecordID= 366");
        }
        
        public override void Down()
        {
        }
    }
}

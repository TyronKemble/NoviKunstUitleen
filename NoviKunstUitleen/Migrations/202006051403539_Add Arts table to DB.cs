namespace NoviKunstUitleen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddArtstabletoDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
               "dbo.Arts",
               c => new
               {
                   ArtsId = c.Int(nullable: false, identity: true),
                   Name = c.String(nullable: false, maxLength: 20),
                   Image = c.String(),
                   Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                   NumbersInStock = c.Int(nullable: false),
                   NumbersAvailable = c.Int(nullable: false),
                   DateAdded = c.String(nullable: false),
                   Creator = c.String(nullable: false),
               })
               .PrimaryKey(t => t.ArtsId);
        }
        
        public override void Down()
        {
            DropTable("dbo.Arts");
        }
    }
}

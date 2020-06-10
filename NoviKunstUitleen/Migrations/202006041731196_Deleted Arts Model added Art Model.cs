namespace NoviKunstUitleen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeletedArtsModeladdedArtModel : DbMigration
    {
        public override void Up()
        {
            //DropForeignKey("dbo.Carts", "ArtsId", "dbo.Arts");
            //DropForeignKey("dbo.ArtLendPeriods", "ArtsId", "dbo.Arts");
            //DropForeignKey("dbo.OrderDetails", "ArtsId", "dbo.Arts");
            //DropIndex("dbo.Carts", new[] { "ArtsId" });
            //DropIndex("dbo.ArtLendPeriods", new[] { "ArtsId" });
            //DropIndex("dbo.OrderDetails", new[] { "ArtsId" });
            //AddColumn("dbo.Carts", "ArtId", c => c.Int(nullable: false));
            //AddColumn("dbo.ArtLendPeriods", "ArtId", c => c.Int());
            //AddColumn("dbo.OrderDetails", "ArtId", c => c.Int(nullable: false));
            //CreateIndex("dbo.Carts", "ArtId");
            //CreateIndex("dbo.ArtLendPeriods", "ArtId");
            //CreateIndex("dbo.OrderDetails", "ArtId");
            //AddForeignKey("dbo.Carts", "ArtId", "dbo.Arts", "ArtId", cascadeDelete: true);
            //AddForeignKey("dbo.ArtLendPeriods", "ArtId", "dbo.Arts", "ArtId");
            //AddForeignKey("dbo.OrderDetails", "ArtId", "dbo.Arts", "ArtId", cascadeDelete: true);
            //DropColumn("dbo.Carts", "ArtsId");
            //DropColumn("dbo.ArtLendPeriods", "ArtsId");
            //DropColumn("dbo.OrderDetails", "ArtsId");
        }
        
        public override void Down()
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
            
          
            //AddColumn("dbo.OrderDetails", "ArtsId", c => c.Int(nullable: false));
            //AddColumn("dbo.ArtLendPeriods", "ArtsId", c => c.Int());
            //AddColumn("dbo.Carts", "ArtsId", c => c.Int(nullable: false));
            //DropForeignKey("dbo.OrderDetails", "ArtId", "dbo.Arts");
            //DropForeignKey("dbo.ArtLendPeriods", "ArtId", "dbo.Arts");
            //DropForeignKey("dbo.Carts", "ArtId", "dbo.Arts");
            //DropIndex("dbo.OrderDetails", new[] { "ArtId" });
            //DropIndex("dbo.ArtLendPeriods", new[] { "ArtId" });
            //DropIndex("dbo.Carts", new[] { "ArtId" });
            //DropColumn("dbo.OrderDetails", "ArtId");
            //DropColumn("dbo.ArtLendPeriods", "ArtId");
            //DropColumn("dbo.Carts", "ArtId");
            //CreateIndex("dbo.OrderDetails", "ArtsId");
            //CreateIndex("dbo.ArtLendPeriods", "ArtsId");
            //CreateIndex("dbo.Carts", "ArtsId");
            //AddForeignKey("dbo.OrderDetails", "ArtsId", "dbo.Arts", "ArtsId", cascadeDelete: true);
            //AddForeignKey("dbo.ArtLendPeriods", "ArtsId", "dbo.Arts", "ArtsId");
            //AddForeignKey("dbo.Carts", "ArtsId", "dbo.Arts", "ArtsId", cascadeDelete: true);
        }
    }
}

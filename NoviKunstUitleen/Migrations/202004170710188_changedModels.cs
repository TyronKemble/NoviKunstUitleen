namespace NoviKunstUitleen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Carts",
                c => new
                    {
                        RecordId = c.Int(nullable: false, identity: true),
                        CartId = c.String(),
                        AlbumId = c.Int(nullable: false),
                        Count = c.Int(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        Arts_Id = c.Int(),
                    })
                .PrimaryKey(t => t.RecordId)
                .ForeignKey("dbo.Arts", t => t.Arts_Id)
                .Index(t => t.Arts_Id);
            
            CreateTable(
                "dbo.OrderDetails",
                c => new
                    {
                        OrderDetailId = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(nullable: false),
                        AlbumId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        UnitPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Arts_Id = c.Int(),
                    })
                .PrimaryKey(t => t.OrderDetailId)
                .ForeignKey("dbo.Arts", t => t.Arts_Id)
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .Index(t => t.OrderId)
                .Index(t => t.Arts_Id);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Address = c.String(),
                        City = c.String(),
                        State = c.String(),
                        PostalCode = c.String(),
                        Country = c.String(),
                        Phone = c.String(),
                        Email = c.String(),
                        Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OrderDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.OrderId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderDetails", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.OrderDetails", "Arts_Id", "dbo.Arts");
            DropForeignKey("dbo.Carts", "Arts_Id", "dbo.Arts");
            DropIndex("dbo.OrderDetails", new[] { "Arts_Id" });
            DropIndex("dbo.OrderDetails", new[] { "OrderId" });
            DropIndex("dbo.Carts", new[] { "Arts_Id" });
            DropTable("dbo.Orders");
            DropTable("dbo.OrderDetails");
            DropTable("dbo.Carts");
        }
    }
}

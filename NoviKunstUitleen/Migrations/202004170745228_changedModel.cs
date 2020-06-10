namespace NoviKunstUitleen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Carts", "Arts_Id", "dbo.Arts");
            DropForeignKey("dbo.OrderDetails", "Arts_Id", "dbo.Arts");
            DropIndex("dbo.Carts", new[] { "Arts_Id" });
            DropIndex("dbo.OrderDetails", new[] { "Arts_Id" });
            RenameColumn(table: "dbo.Carts", name: "Arts_Id", newName: "ArtsId");
            RenameColumn(table: "dbo.OrderDetails", name: "Arts_Id", newName: "ArtsId");
            AddColumn("dbo.Arts", "ArtsId", c => c.Int(nullable: false));
            AlterColumn("dbo.Carts", "ArtsId", c => c.Int(nullable: false));
            AlterColumn("dbo.OrderDetails", "ArtsId", c => c.Int(nullable: false));
            CreateIndex("dbo.Carts", "ArtsId");
            CreateIndex("dbo.OrderDetails", "ArtsId");
            AddForeignKey("dbo.Carts", "ArtsId", "dbo.Arts", "Id", cascadeDelete: true);
            AddForeignKey("dbo.OrderDetails", "ArtsId", "dbo.Arts", "Id", cascadeDelete: true);
            DropColumn("dbo.Carts", "AlbumId");
            DropColumn("dbo.OrderDetails", "AlbumId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OrderDetails", "AlbumId", c => c.Int(nullable: false));
            AddColumn("dbo.Carts", "AlbumId", c => c.Int(nullable: false));
            DropForeignKey("dbo.OrderDetails", "ArtsId", "dbo.Arts");
            DropForeignKey("dbo.Carts", "ArtsId", "dbo.Arts");
            DropIndex("dbo.OrderDetails", new[] { "ArtsId" });
            DropIndex("dbo.Carts", new[] { "ArtsId" });
            AlterColumn("dbo.OrderDetails", "ArtsId", c => c.Int());
            AlterColumn("dbo.Carts", "ArtsId", c => c.Int());
            DropColumn("dbo.Arts", "ArtsId");
            RenameColumn(table: "dbo.OrderDetails", name: "ArtsId", newName: "Arts_Id");
            RenameColumn(table: "dbo.Carts", name: "ArtsId", newName: "Arts_Id");
            CreateIndex("dbo.OrderDetails", "Arts_Id");
            CreateIndex("dbo.Carts", "Arts_Id");
            AddForeignKey("dbo.OrderDetails", "Arts_Id", "dbo.Arts", "Id");
            AddForeignKey("dbo.Carts", "Arts_Id", "dbo.Arts", "Id");
        }
    }
}

namespace NoviKunstUitleen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeletedIDfromdbArts : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Carts", "ArtsId", "dbo.Arts");
            DropForeignKey("dbo.OrderDetails", "ArtsId", "dbo.Arts");
            DropPrimaryKey("dbo.Arts");
            AlterColumn("dbo.Arts", "ArtsId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Arts", "ArtsId");
            AddForeignKey("dbo.Carts", "ArtsId", "dbo.Arts", "ArtsId", cascadeDelete: true);
            AddForeignKey("dbo.OrderDetails", "ArtsId", "dbo.Arts", "ArtsId", cascadeDelete: true);
            DropColumn("dbo.Arts", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Arts", "Id", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.OrderDetails", "ArtsId", "dbo.Arts");
            DropForeignKey("dbo.Carts", "ArtsId", "dbo.Arts");
            DropPrimaryKey("dbo.Arts");
            AlterColumn("dbo.Arts", "ArtsId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Arts", "Id");
            AddForeignKey("dbo.OrderDetails", "ArtsId", "dbo.Arts", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Carts", "ArtsId", "dbo.Arts", "Id", cascadeDelete: true);
        }
    }
}

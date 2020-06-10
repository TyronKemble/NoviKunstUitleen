namespace NoviKunstUitleen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedCheckOutLogic : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Orders", "FirstName", c => c.String(nullable: false, maxLength: 160));
            AlterColumn("dbo.Orders", "LastName", c => c.String(nullable: false, maxLength: 160));
            AlterColumn("dbo.Orders", "Address", c => c.String(nullable: false, maxLength: 70));
            AlterColumn("dbo.Orders", "City", c => c.String(nullable: false, maxLength: 40));
            AlterColumn("dbo.Orders", "State", c => c.String(nullable: false, maxLength: 40));
            AlterColumn("dbo.Orders", "PostalCode", c => c.String(nullable: false, maxLength: 10));
            AlterColumn("dbo.Orders", "Country", c => c.String(nullable: false, maxLength: 40));
            AlterColumn("dbo.Orders", "Phone", c => c.String(nullable: false, maxLength: 24));
            AlterColumn("dbo.Orders", "Email", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "Email", c => c.String());
            AlterColumn("dbo.Orders", "Phone", c => c.String());
            AlterColumn("dbo.Orders", "Country", c => c.String());
            AlterColumn("dbo.Orders", "PostalCode", c => c.String());
            AlterColumn("dbo.Orders", "State", c => c.String());
            AlterColumn("dbo.Orders", "City", c => c.String());
            AlterColumn("dbo.Orders", "Address", c => c.String());
            AlterColumn("dbo.Orders", "LastName", c => c.String());
            AlterColumn("dbo.Orders", "FirstName", c => c.String());
        }
    }
}

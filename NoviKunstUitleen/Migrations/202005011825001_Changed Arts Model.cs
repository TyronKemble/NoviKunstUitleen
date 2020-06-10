namespace NoviKunstUitleen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedArtsModel : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Arts", "Name", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.Arts", "Image", c => c.String(nullable: false));
            AlterColumn("dbo.Arts", "Price", c => c.Byte(nullable: false));
            AlterColumn("dbo.Arts", "DateAdded", c => c.String(nullable: false));
            AlterColumn("dbo.Arts", "Creator", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Arts", "Creator", c => c.String());
            AlterColumn("dbo.Arts", "DateAdded", c => c.String());
            AlterColumn("dbo.Arts", "Price", c => c.Int(nullable: false));
            AlterColumn("dbo.Arts", "Image", c => c.String());
            AlterColumn("dbo.Arts", "Name", c => c.String());
        }
    }
}

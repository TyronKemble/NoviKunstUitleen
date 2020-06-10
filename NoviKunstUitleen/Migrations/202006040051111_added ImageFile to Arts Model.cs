namespace NoviKunstUitleen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedImageFiletoArtsModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Arts", "ImageFile", c => c.Binary());
            AlterColumn("dbo.Arts", "Image", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Arts", "Image", c => c.String(nullable: false));
            DropColumn("dbo.Arts", "ImageFile");
        }
    }
}

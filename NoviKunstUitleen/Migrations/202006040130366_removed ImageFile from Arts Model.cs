namespace NoviKunstUitleen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removedImageFilefromArtsModel : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Arts", "ImageFile");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Arts", "ImageFile", c => c.Binary());
        }
    }
}

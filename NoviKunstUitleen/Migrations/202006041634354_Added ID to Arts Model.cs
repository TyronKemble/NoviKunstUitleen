namespace NoviKunstUitleen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedIDtoArtsModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Arts", "Id", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Arts", "Id");
        }
    }
}

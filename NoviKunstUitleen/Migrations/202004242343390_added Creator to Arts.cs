namespace NoviKunstUitleen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedCreatortoArts : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Arts", "Creator", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Arts", "Creator");
        }
    }
}

namespace NoviKunstUitleen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeletedArtsDB : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.Arts");
        }
        
        public override void Down()
        {
        }
    }
}

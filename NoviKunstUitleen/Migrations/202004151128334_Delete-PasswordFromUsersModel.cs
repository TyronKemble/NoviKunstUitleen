namespace NoviKunstUitleen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeletePasswordFromUsersModel : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.StaffMembers", "Password");
            DropColumn("dbo.Students", "Password");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Students", "Password", c => c.String());
            AddColumn("dbo.StaffMembers", "Password", c => c.String());
        }
    }
}

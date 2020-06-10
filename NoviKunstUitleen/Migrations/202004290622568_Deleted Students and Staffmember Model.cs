namespace NoviKunstUitleen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeletedStudentsandStaffmemberModel : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.StaffMembers");
        }
        
        public override void Down()
        {

            CreateTable(
                "dbo.StaffMembers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        Lastname = c.String(),
                        Email = c.String(),
                        CryptoWallet = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
    }
}

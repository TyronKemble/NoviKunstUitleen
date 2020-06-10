namespace NoviKunstUitleen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedModelsStaffmemberAndStudents : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StaffMembers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        Lastname = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                        CryptoUWallet = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        Lastname = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                        CryptoUWallet = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Students");
            DropTable("dbo.StaffMembers");
        }
    }
}

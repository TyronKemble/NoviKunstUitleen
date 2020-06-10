namespace NoviKunstUitleen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class artsModelAddedToDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Arts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Image = c.String(),
                        Price = c.Int(nullable: false),
                        NumbersInStock = c.Int(nullable: false),
                        NumbersAvailable = c.Int(nullable: false),
                        DateAdded = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Arts");
        }
    }
}

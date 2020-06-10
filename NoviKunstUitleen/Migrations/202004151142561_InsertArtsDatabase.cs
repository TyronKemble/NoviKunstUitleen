namespace NoviKunstUitleen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InsertArtsDatabase : DbMigration
    {
        public override void Up()
        {
            Sql("SET IDENTITY_INSERT Arts ON " + 
                "INSERT INTO ARTS (Id, Name, Image, Price, NumbersInStock, NumbersAvailable, DateAdded) VALUES (1,'Picasso','https://images.unsplash.com/photo-1533468432434-200de3b5d528?ixlib=rb-1.2.1&ixid=eyJhcHBfaWQiOjEyMDd9&auto=format&fit=crop&w=150&q=60', 12, 10,10,'20/10/2009')" +
                "INSERT INTO ARTS (Id, Name, Image, Price, NumbersInStock, NumbersAvailable, DateAdded) VALUES (2,'Mystery','https://images.unsplash.com/photo-1551913902-c92207136625?ixlib=rb-1.2.1&ixid=eyJhcHBfaWQiOjEyMDd9&auto=format&fit=crop&w=150&q=60', 5, 5,5,'10/10/2017')"+
                "INSERT INTO ARTS (Id, Name, Image, Price, NumbersInStock, NumbersAvailable, DateAdded) VALUES (3,'High Wild','https://images.unsplash.com/photo-1586763365361-dfe8a05f66b6?ixlib=rb-1.2.1&ixid=eyJhcHBfaWQiOjEyMDd9&auto=format&fit=crop&w=150&q=60', 2, 3,3,'09/10/2018')"+
                "SET IDENTITY_INSERT Arts Off ");

        }
        
        public override void Down()
        {
        }
    }
}

namespace MVCLabb.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Albums",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                        UserID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Content = c.String(),
                        Date = c.DateTime(nullable: false),
                        UserID = c.Guid(nullable: false),
                        PhotoID = c.Guid(),
                        AlbumID = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Albums", t => t.AlbumID)
                .ForeignKey("dbo.Photos", t => t.PhotoID)
                .ForeignKey("dbo.Users", t => t.UserID)
                .Index(t => t.UserID)
                .Index(t => t.PhotoID)
                .Index(t => t.AlbumID);
            
            CreateTable(
                "dbo.Photos",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        Path = c.String(),
                        Description = c.String(),
                        Date = c.DateTime(nullable: false),
                        UserID = c.Guid(nullable: false),
                        AlbumID = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Albums", t => t.AlbumID)
                .ForeignKey("dbo.Users", t => t.UserID)
                .Index(t => t.UserID)
                .Index(t => t.AlbumID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Email = c.String(),
                        Password = c.String(),
                        Name = c.String(),
                        Country = c.String(),
                        Admin = c.Boolean(nullable: false),
                        Salt = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Photos", "UserID", "dbo.Users");
            DropForeignKey("dbo.Comments", "UserID", "dbo.Users");
            DropForeignKey("dbo.Albums", "UserID", "dbo.Users");
            DropForeignKey("dbo.Comments", "PhotoID", "dbo.Photos");
            DropForeignKey("dbo.Photos", "AlbumID", "dbo.Albums");
            DropForeignKey("dbo.Comments", "AlbumID", "dbo.Albums");
            DropIndex("dbo.Photos", new[] { "AlbumID" });
            DropIndex("dbo.Photos", new[] { "UserID" });
            DropIndex("dbo.Comments", new[] { "AlbumID" });
            DropIndex("dbo.Comments", new[] { "PhotoID" });
            DropIndex("dbo.Comments", new[] { "UserID" });
            DropIndex("dbo.Albums", new[] { "UserID" });
            DropTable("dbo.Users");
            DropTable("dbo.Photos");
            DropTable("dbo.Comments");
            DropTable("dbo.Albums");
        }
    }
}

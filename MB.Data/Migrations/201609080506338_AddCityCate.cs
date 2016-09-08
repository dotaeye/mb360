namespace MB.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCityCate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CityCate",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        ParentId = c.Int(),
                        DisplayOrder = c.Int(nullable: false),
                        Code = c.String(nullable: false, maxLength: 50),
                        CreateUserId = c.String(),
                        CreateTime = c.DateTime(),
                        LastUserId = c.String(),
                        LastTime = c.DateTime(),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CityCate", t => t.ParentId)
                .Index(t => t.ParentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CityCate", "ParentId", "dbo.CityCate");
            DropIndex("dbo.CityCate", new[] { "ParentId" });
            DropTable("dbo.CityCate");
        }
    }
}

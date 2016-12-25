namespace MB.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCarMapping : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CarMapping",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CarCateId = c.Int(nullable: false),
                        UserId = c.String(nullable: false, maxLength: 128),
                        CarCateList = c.String(maxLength: 100),
                        Name = c.String(maxLength: 100),
                        Default = c.Boolean(nullable: false),
                        Delete = c.Boolean(nullable: false),
                        DisplayOrder = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CarCate", t => t.CarCateId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.CarCateId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CarMapping", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.CarMapping", "CarCateId", "dbo.CarCate");
            DropIndex("dbo.CarMapping", new[] { "UserId" });
            DropIndex("dbo.CarMapping", new[] { "CarCateId" });
            DropTable("dbo.CarMapping");
        }
    }
}

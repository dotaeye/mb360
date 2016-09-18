namespace MB.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProductCarCateMap : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductCarCate",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        CarCateId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CarCate", t => t.CarCateId, cascadeDelete: true)
                .ForeignKey("dbo.Product", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.CarCateId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductCarCate", "ProductId", "dbo.Product");
            DropForeignKey("dbo.ProductCarCate", "CarCateId", "dbo.CarCate");
            DropIndex("dbo.ProductCarCate", new[] { "CarCateId" });
            DropIndex("dbo.ProductCarCate", new[] { "ProductId" });
            DropTable("dbo.ProductCarCate");
        }
    }
}

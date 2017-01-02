namespace MB.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RefactorManufactureOneToOne : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProductManufacturer", "ManufacturerId", "dbo.Manufacturer");
            DropForeignKey("dbo.ProductManufacturer", "ProductId", "dbo.Product");
            DropIndex("dbo.ProductManufacturer", new[] { "ProductId" });
            DropIndex("dbo.ProductManufacturer", new[] { "ManufacturerId" });
            AddColumn("dbo.Product", "ManufacturerId", c => c.Int());
            AddColumn("dbo.Manufacturer", "DisplayOrder", c => c.Int(nullable: false));
            CreateIndex("dbo.Product", "ManufacturerId");
            AddForeignKey("dbo.Product", "ManufacturerId", "dbo.Manufacturer", "Id");
            DropTable("dbo.ProductManufacturer");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ProductManufacturer",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        ManufacturerId = c.Int(nullable: false),
                        IsFeaturedProduct = c.Boolean(nullable: false),
                        DisplayOrder = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.Product", "ManufacturerId", "dbo.Manufacturer");
            DropIndex("dbo.Product", new[] { "ManufacturerId" });
            DropColumn("dbo.Manufacturer", "DisplayOrder");
            DropColumn("dbo.Product", "ManufacturerId");
            CreateIndex("dbo.ProductManufacturer", "ManufacturerId");
            CreateIndex("dbo.ProductManufacturer", "ProductId");
            AddForeignKey("dbo.ProductManufacturer", "ProductId", "dbo.Product", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ProductManufacturer", "ManufacturerId", "dbo.Manufacturer", "Id", cascadeDelete: true);
        }
    }
}

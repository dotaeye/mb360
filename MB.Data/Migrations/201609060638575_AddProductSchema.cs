namespace MB.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProductSchema : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "UserPermission_Id", "dbo.UserPermission");
            DropIndex("dbo.AspNetUsers", new[] { "UserPermission_Id" });
            CreateTable(
                "dbo.CarCate",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        ParentId = c.Int(),
                        Code = c.String(nullable: false, maxLength: 50),
                        CreateUserId = c.String(),
                        CreateTime = c.DateTime(),
                        LastUserId = c.String(),
                        LastTime = c.DateTime(),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CarCate", t => t.ParentId)
                .Index(t => t.ParentId);
            
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        ParentId = c.Int(),
                        Code = c.String(nullable: false, maxLength: 50),
                        CreateUserId = c.String(),
                        CreateTime = c.DateTime(),
                        LastUserId = c.String(),
                        LastTime = c.DateTime(),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Category", t => t.ParentId)
                .Index(t => t.ParentId);
            
            CreateTable(
                "dbo.Manufacturer",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 200),
                        Description = c.String(maxLength: 400),
                        ImageUrl = c.String(maxLength: 800),
                        CreateUserId = c.String(),
                        CreateTime = c.DateTime(),
                        LastUserId = c.String(),
                        LastTime = c.DateTime(),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProductAttribute",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                        CreateUserId = c.String(),
                        CreateTime = c.DateTime(),
                        LastUserId = c.String(),
                        LastTime = c.DateTime(),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProductAttributeMapping",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        ProductAttributeId = c.Int(nullable: false),
                        AttributeControlTypeId = c.Int(nullable: false),
                        DisplayOrder = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Product", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.ProductAttribute", t => t.ProductAttributeId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.ProductAttributeId);
            
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 200),
                        CategoryId = c.Int(nullable: false),
                        SKU = c.String(maxLength: 400),
                        isAgreeActive = c.Boolean(nullable: false),
                        Status = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 4),
                        VipPrice = c.Decimal(nullable: false, precision: 18, scale: 4),
                        ImageUrl = c.String(maxLength: 800),
                        CreateUserId = c.String(),
                        CreateTime = c.DateTime(),
                        LastUserId = c.String(),
                        LastTime = c.DateTime(),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Category", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Manufacturer", t => t.ManufacturerId, cascadeDelete: true)
                .ForeignKey("dbo.Product", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.ManufacturerId);
            
            CreateTable(
                "dbo.ProductSpecificationAttribute",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        SpecificationAttributeOptionId = c.Int(nullable: false),
                        DisplayOrder = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Product", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.SpecificationAttributeOption", t => t.SpecificationAttributeOptionId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.SpecificationAttributeOptionId);
            
            CreateTable(
                "dbo.SpecificationAttributeOption",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SpecificationAttributeId = c.Int(nullable: false),
                        Name = c.String(nullable: false),
                        DisplayOrder = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SpecificationAttribute", t => t.SpecificationAttributeId, cascadeDelete: true)
                .Index(t => t.SpecificationAttributeId);
            
            CreateTable(
                "dbo.SpecificationAttribute",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        DisplayOrder = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProductStorageQuantity",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        StorageId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        CreateUserId = c.String(),
                        CreateTime = c.DateTime(),
                        LastUserId = c.String(),
                        LastTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Product", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.Storage", t => t.StorageId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.StorageId);
            
            CreateTable(
                "dbo.Storage",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 200),
                        Location = c.Geography(nullable: false),
                        CreateUserId = c.String(),
                        CreateTime = c.DateTime(),
                        LastUserId = c.String(),
                        LastTime = c.DateTime(),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProductAttributeValue",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 400),
                        ProductAttributeMappingId = c.Int(nullable: false),
                        PriceAdjustment = c.Decimal(nullable: false, precision: 18, scale: 4),
                        ImageUrl = c.String(),
                        CreateUserId = c.String(),
                        CreateTime = c.DateTime(),
                        LastUserId = c.String(),
                        LastTime = c.DateTime(),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProductAttributeMapping", t => t.ProductAttributeMappingId, cascadeDelete: true)
                .Index(t => t.ProductAttributeMappingId);
            
            DropColumn("dbo.AspNetUsers", "UserPermission_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "UserPermission_Id", c => c.Int());
            DropForeignKey("dbo.ProductAttributeValue", "ProductAttributeMappingId", "dbo.ProductAttributeMapping");
            DropForeignKey("dbo.ProductAttributeMapping", "ProductAttributeId", "dbo.ProductAttribute");
            DropForeignKey("dbo.ProductAttributeMapping", "ProductId", "dbo.Product");
            DropForeignKey("dbo.ProductStorageQuantity", "StorageId", "dbo.Storage");
            DropForeignKey("dbo.ProductStorageQuantity", "ProductId", "dbo.Product");
            DropForeignKey("dbo.ProductSpecificationAttribute", "SpecificationAttributeOptionId", "dbo.SpecificationAttributeOption");
            DropForeignKey("dbo.SpecificationAttributeOption", "SpecificationAttributeId", "dbo.SpecificationAttribute");
            DropForeignKey("dbo.ProductSpecificationAttribute", "ProductId", "dbo.Product");
            DropForeignKey("dbo.ProductManufacturer", "ProductId", "dbo.Product");
            DropForeignKey("dbo.ProductManufacturer", "ManufacturerId", "dbo.Manufacturer");
            DropForeignKey("dbo.Product", "CategoryId", "dbo.Category");
            DropForeignKey("dbo.Category", "ParentId", "dbo.Category");
            DropForeignKey("dbo.CarCate", "ParentId", "dbo.CarCate");
            DropIndex("dbo.ProductAttributeValue", new[] { "ProductAttributeMappingId" });
            DropIndex("dbo.ProductStorageQuantity", new[] { "StorageId" });
            DropIndex("dbo.ProductStorageQuantity", new[] { "ProductId" });
            DropIndex("dbo.SpecificationAttributeOption", new[] { "SpecificationAttributeId" });
            DropIndex("dbo.ProductSpecificationAttribute", new[] { "SpecificationAttributeOptionId" });
            DropIndex("dbo.ProductSpecificationAttribute", new[] { "ProductId" });
            DropIndex("dbo.ProductManufacturer", new[] { "ManufacturerId" });
            DropIndex("dbo.ProductManufacturer", new[] { "ProductId" });
            DropIndex("dbo.Product", new[] { "CategoryId" });
            DropIndex("dbo.ProductAttributeMapping", new[] { "ProductAttributeId" });
            DropIndex("dbo.ProductAttributeMapping", new[] { "ProductId" });
            DropIndex("dbo.Category", new[] { "ParentId" });
            DropIndex("dbo.CarCate", new[] { "ParentId" });
            DropTable("dbo.ProductAttributeValue");
            DropTable("dbo.Storage");
            DropTable("dbo.ProductStorageQuantity");
            DropTable("dbo.SpecificationAttribute");
            DropTable("dbo.SpecificationAttributeOption");
            DropTable("dbo.ProductSpecificationAttribute");
            DropTable("dbo.ProductManufacturer");
            DropTable("dbo.Product");
            DropTable("dbo.ProductAttributeMapping");
            DropTable("dbo.ProductAttribute");
            DropTable("dbo.Manufacturer");
            DropTable("dbo.Category");
            DropTable("dbo.CarCate");
            CreateIndex("dbo.AspNetUsers", "UserPermission_Id");
            AddForeignKey("dbo.AspNetUsers", "UserPermission_Id", "dbo.UserPermission", "Id");
        }
    }
}

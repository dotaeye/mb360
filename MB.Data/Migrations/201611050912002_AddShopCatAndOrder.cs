namespace MB.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddShopCatAndOrder : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ShoppingCartItem",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OwnerId = c.String(),
                        StorageId = c.Int(nullable: false),
                        ShoppingCartTypeId = c.Int(nullable: false),
                        CustomerId = c.String(nullable: false, maxLength: 128),
                        ProductId = c.Int(nullable: false),
                        AttributesXml = c.String(),
                        Quantity = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        CreateTime = c.DateTime(),
                        LastTime = c.DateTime(),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.Product", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.CustomerId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Banner",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Url = c.String(),
                        ImageUrl = c.String(maxLength: 250),
                        NativeRoute = c.Boolean(nullable: false),
                        DisplayOrder = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        CreateUserId = c.String(),
                        CreateTime = c.DateTime(),
                        LastUserId = c.String(),
                        LastTime = c.DateTime(),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Order",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderGuid = c.Guid(nullable: false),
                        OwnerId = c.String(),
                        CustomerId = c.String(nullable: false, maxLength: 128),
                        AddressId = c.Int(nullable: false),
                        PickUpInStore = c.Boolean(nullable: false),
                        OrderStatusId = c.Int(nullable: false),
                        PaymentMethodSystemName = c.String(),
                        PaymentMethodDesction = c.String(),
                        OrderTotalExclShipping = c.Decimal(nullable: false, precision: 18, scale: 4),
                        OrderShipping = c.Decimal(nullable: false, precision: 18, scale: 4),
                        OrderTotal = c.Decimal(nullable: false, precision: 18, scale: 8),
                        CustomerIp = c.String(),
                        PaidDate = c.DateTime(),
                        ShippingMethod = c.String(),
                        Deleted = c.Boolean(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        TrackingNumber = c.String(),
                        ShippedTime = c.DateTime(),
                        DeliveryTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Address", t => t.AddressId)
                .ForeignKey("dbo.AspNetUsers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId)
                .Index(t => t.AddressId);
            
            CreateTable(
                "dbo.Address",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        PhoneNumber = c.String(),
                        CityId = c.Int(nullable: false),
                        Detail = c.String(),
                        Default = c.Boolean(nullable: false),
                        CreateUserId = c.String(),
                        CreateTime = c.DateTime(),
                        LastUserId = c.String(),
                        LastTime = c.DateTime(),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CityCate", t => t.CityId)
                .Index(t => t.CityId);
            
            CreateTable(
                "dbo.OrderItem",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderItemGuid = c.Guid(nullable: false),
                        OrderId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        UnitPrice = c.Decimal(nullable: false, precision: 18, scale: 4),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 4),
                        AttributeDescription = c.String(),
                        AttributesXml = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Order", t => t.OrderId, cascadeDelete: true)
                .ForeignKey("dbo.Product", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.OrderId)
                .Index(t => t.ProductId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderItem", "ProductId", "dbo.Product");
            DropForeignKey("dbo.OrderItem", "OrderId", "dbo.Order");
            DropForeignKey("dbo.Order", "CustomerId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Order", "AddressId", "dbo.Address");
            DropForeignKey("dbo.Address", "CityId", "dbo.CityCate");
            DropForeignKey("dbo.ShoppingCartItem", "ProductId", "dbo.Product");
            DropForeignKey("dbo.ShoppingCartItem", "CustomerId", "dbo.AspNetUsers");
            DropIndex("dbo.OrderItem", new[] { "ProductId" });
            DropIndex("dbo.OrderItem", new[] { "OrderId" });
            DropIndex("dbo.Address", new[] { "CityId" });
            DropIndex("dbo.Order", new[] { "AddressId" });
            DropIndex("dbo.Order", new[] { "CustomerId" });
            DropIndex("dbo.ShoppingCartItem", new[] { "ProductId" });
            DropIndex("dbo.ShoppingCartItem", new[] { "CustomerId" });
            DropTable("dbo.OrderItem");
            DropTable("dbo.Address");
            DropTable("dbo.Order");
            DropTable("dbo.Banner");
            DropTable("dbo.ShoppingCartItem");
        }
    }
}

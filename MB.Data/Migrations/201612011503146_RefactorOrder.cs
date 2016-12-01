namespace MB.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RefactorOrder : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OrderItem", "OrderId", "dbo.Order");
            DropForeignKey("dbo.OrderItem", "ProductId", "dbo.Product");
            DropIndex("dbo.OrderItem", new[] { "OrderId" });
            DropIndex("dbo.OrderItem", new[] { "ProductId" });
            CreateTable(
                "dbo.Package",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AddressId = c.Int(nullable: false),
                        LogisticCompany = c.String(),
                        LogisticCode = c.String(),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 8),
                        Weight = c.Decimal(nullable: false, precision: 18, scale: 4),
                        Distance = c.Decimal(nullable: false, precision: 18, scale: 4),
                        OrderId = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Address", t => t.AddressId)
                .ForeignKey("dbo.Order", t => t.OrderId, cascadeDelete: true)
                .Index(t => t.AddressId)
                .Index(t => t.OrderId);
            
            AddColumn("dbo.ShoppingCartItem", "OrderId", c => c.Int());
            AddColumn("dbo.ShoppingCartItem", "PackageId", c => c.Int());
            AddColumn("dbo.ShoppingCartItem", "UnitPrice", c => c.Decimal(nullable: false, precision: 18, scale: 4));
            AddColumn("dbo.ShoppingCartItem", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 4));
            CreateIndex("dbo.ShoppingCartItem", "OrderId");
            CreateIndex("dbo.ShoppingCartItem", "PackageId");
            AddForeignKey("dbo.ShoppingCartItem", "OrderId", "dbo.Order", "Id");
            AddForeignKey("dbo.ShoppingCartItem", "PackageId", "dbo.Package", "Id");
            DropTable("dbo.OrderItem");
        }
        
        public override void Down()
        {
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
                        TrackingNumber = c.String(),
                        Deleted = c.Boolean(nullable: false),
                        ShippedTime = c.DateTime(),
                        DeliveryTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.ShoppingCartItem", "PackageId", "dbo.Package");
            DropForeignKey("dbo.Package", "OrderId", "dbo.Order");
            DropForeignKey("dbo.Package", "AddressId", "dbo.Address");
            DropForeignKey("dbo.ShoppingCartItem", "OrderId", "dbo.Order");
            DropIndex("dbo.Package", new[] { "OrderId" });
            DropIndex("dbo.Package", new[] { "AddressId" });
            DropIndex("dbo.ShoppingCartItem", new[] { "PackageId" });
            DropIndex("dbo.ShoppingCartItem", new[] { "OrderId" });
            DropColumn("dbo.ShoppingCartItem", "Price");
            DropColumn("dbo.ShoppingCartItem", "UnitPrice");
            DropColumn("dbo.ShoppingCartItem", "PackageId");
            DropColumn("dbo.ShoppingCartItem", "OrderId");
            DropTable("dbo.Package");
            CreateIndex("dbo.OrderItem", "ProductId");
            CreateIndex("dbo.OrderItem", "OrderId");
            AddForeignKey("dbo.OrderItem", "ProductId", "dbo.Product", "Id", cascadeDelete: true);
            AddForeignKey("dbo.OrderItem", "OrderId", "dbo.Order", "Id", cascadeDelete: true);
        }
    }
}

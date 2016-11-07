namespace MB.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyOrder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderItem", "TrackingNumber", c => c.String());
            AddColumn("dbo.OrderItem", "ShippedTime", c => c.DateTime());
            AddColumn("dbo.OrderItem", "DeliveryTime", c => c.DateTime());
            DropColumn("dbo.Order", "OwnerId");
            DropColumn("dbo.Order", "TrackingNumber");
            DropColumn("dbo.Order", "ShippedTime");
            DropColumn("dbo.Order", "DeliveryTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Order", "DeliveryTime", c => c.DateTime());
            AddColumn("dbo.Order", "ShippedTime", c => c.DateTime());
            AddColumn("dbo.Order", "TrackingNumber", c => c.String());
            AddColumn("dbo.Order", "OwnerId", c => c.String());
            DropColumn("dbo.OrderItem", "DeliveryTime");
            DropColumn("dbo.OrderItem", "ShippedTime");
            DropColumn("dbo.OrderItem", "TrackingNumber");
        }
    }
}

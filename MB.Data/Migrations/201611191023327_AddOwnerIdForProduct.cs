namespace MB.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOwnerIdForProduct : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Product", "OwenerId", c => c.String(maxLength: 128));
            AlterColumn("dbo.ShoppingCartItem", "OwnerId", c => c.String(maxLength: 128));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ShoppingCartItem", "OwnerId", c => c.String());
            DropColumn("dbo.Product", "OwenerId");
        }
    }
}

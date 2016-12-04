namespace MB.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNameForShopCart : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ShoppingCartItem", "Name", c => c.String(maxLength: 250));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ShoppingCartItem", "Name");
        }
    }
}

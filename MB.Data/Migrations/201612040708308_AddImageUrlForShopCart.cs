namespace MB.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddImageUrlForShopCart : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ShoppingCartItem", "ImageUrl", c => c.String(maxLength: 250));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ShoppingCartItem", "ImageUrl");
        }
    }
}

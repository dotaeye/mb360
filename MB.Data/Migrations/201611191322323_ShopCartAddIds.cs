namespace MB.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ShopCartAddIds : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ShoppingCartItem", "AttributesIds", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ShoppingCartItem", "AttributesIds");
        }
    }
}

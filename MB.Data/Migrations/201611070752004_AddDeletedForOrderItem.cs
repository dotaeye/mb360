namespace MB.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDeletedForOrderItem : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderItem", "Deleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrderItem", "Deleted");
        }
    }
}

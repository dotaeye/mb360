namespace MB.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUrgencyPrice : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Product", "UrgencyPrice", c => c.Decimal(nullable: false, precision: 18, scale: 4));
            AddColumn("dbo.Product", "Description", c => c.String(maxLength: 800));
            AddColumn("dbo.Product", "DetailUrl", c => c.String(maxLength: 800));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Product", "DetailUrl");
            DropColumn("dbo.Product", "Description");
            DropColumn("dbo.Product", "UrgencyPrice");
        }
    }
}

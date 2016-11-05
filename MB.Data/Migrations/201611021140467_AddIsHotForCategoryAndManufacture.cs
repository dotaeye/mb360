namespace MB.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIsHotForCategoryAndManufacture : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Category", "IsHot", c => c.Boolean(nullable: false));
            AddColumn("dbo.Category", "HotOrder", c => c.Int(nullable: false));
            AddColumn("dbo.Manufacturer", "IsHot", c => c.Boolean(nullable: false));
            AddColumn("dbo.Manufacturer", "HotOrder", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Manufacturer", "HotOrder");
            DropColumn("dbo.Manufacturer", "IsHot");
            DropColumn("dbo.Category", "HotOrder");
            DropColumn("dbo.Category", "IsHot");
        }
    }
}

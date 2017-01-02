namespace MB.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProductIsFeature : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Product", "IsFeaturedProduct", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Product", "IsFeaturedProduct");
        }
    }
}
